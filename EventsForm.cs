using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class EventsForm : Form
    {
        private bool hasSearched = false;

        public EventsForm()
        {
            InitializeComponent();
            LoadCategoryFilter();
            LoadPriorityHighlights();
            LoadAnnouncements();
            DisplayAllUpcomingEvents();
            RefreshRecommendations();
            DisplaySearchHistory();
        }

        private void LoadCategoryFilter()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All");

            foreach (string category in EventRepository.UniqueCategories.OrderBy(c => c))
                cmbCategory.Items.Add(category);

            cmbCategory.SelectedIndex = 0;

            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today.AddMonths(3);
        }

        private void LoadPriorityHighlights()
        {
            lbPriority.Items.Clear();

            List<CommunityEvent> temp = new List<CommunityEvent>();
            while (!EventRepository.PriorityEvents.IsEmpty)
                temp.Add(EventRepository.PriorityEvents.Dequeue());

            foreach (CommunityEvent ev in temp)
                EventRepository.PriorityEvents.Enqueue(ev, (int)ev.Priority);

            foreach (CommunityEvent ev in temp.Take(5))
            {
                string tag = ev.Priority == EventPriority.High ? "HIGH" :
                             ev.Priority == EventPriority.Medium ? "MED" : "LOW";
                lbPriority.Items.Add("[" + tag + "] " + ev.Title + " (" + ev.Date.ToString("dd MMM") + ")");
            }
        }

        private void LoadAnnouncements()
        {
            lbAnnouncements.Items.Clear();

            foreach (CommunityEvent ev in EventRepository.AnnouncementQueue)
                lbAnnouncements.Items.Add(ev.Date.ToString("dd MMM") + " - " + ev.Title);
        }

        private void DisplayAllUpcomingEvents()
        {
            IEnumerable<CommunityEvent> events = EventRepository.EventsByDate
                .Where(kv => kv.Key >= DateTime.Today)
                .SelectMany(kv => kv.Value)
                .OrderBy(e => e.Date);

            ShowInEventList(events);
            hasSearched = false;
        }

        private void ShowInEventList(IEnumerable<CommunityEvent> events)
        {
            lvEvents.BeginUpdate();
            lvEvents.Items.Clear();

            foreach (CommunityEvent ev in events)
            {
                ListViewItem item = new ListViewItem(ev.Title);
                item.SubItems.Add(ev.Category);
                item.SubItems.Add(ev.Date.ToString("dd MMM yyyy HH:mm"));
                item.SubItems.Add(ev.Location);
                item.SubItems.Add(ev.Priority.ToString());
                item.SubItems.Add(ev.IsAnnouncement ? "Announcement" : "Event");
                item.Tag = ev.Id;
                lvEvents.Items.Add(item);
            }

            lvEvents.EndUpdate();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string category = cmbCategory.SelectedItem == null ? "All" : cmbCategory.SelectedItem.ToString();
            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date;
            string keyword = txtKeyword.Text.Trim();

            if (fromDate > toDate)
            {
                MessageBox.Show("The 'from' date cannot be after the 'to' date.",
                    "Invalid date range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EventRepository.SearchHistory.Push(string.Format(
                "{0:HH:mm} [{1}] '{2}' {3:dd MMM}-{4:dd MMM}",
                DateTime.Now, category, keyword, fromDate, toDate));

            if (!category.Equals("All", StringComparison.OrdinalIgnoreCase))
                EventRepository.IncrementSearchFrequency(category);

            foreach (string word in keyword.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                EventRepository.IncrementSearchFrequency(word);

            List<CommunityEvent> results = SearchEvents(category, fromDate, toDate, keyword);
            ShowInEventList(results);
            hasSearched = true;

            RefreshRecommendations();
            DisplaySearchHistory();

            if (results.Count == 0)
            {
                MessageBox.Show("No events matched your search. Try a wider date range or a different keyword.",
                    "No results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private List<CommunityEvent> SearchEvents(string category, DateTime fromDate, DateTime toDate, string keyword)
        {
            List<CommunityEvent> results = new List<CommunityEvent>();

            if (!category.Equals("All", StringComparison.OrdinalIgnoreCase) &&
                EventRepository.EventsByCategory.ContainsKey(category))
            {
                foreach (CommunityEvent ev in EventRepository.EventsByCategory[category])
                {
                    if (ev.Date.Date < fromDate || ev.Date.Date > toDate) continue;
                    if (!MatchesKeyword(ev, keyword)) continue;
                    results.Add(ev);
                }
            }
            else
            {
                foreach (KeyValuePair<DateTime, List<CommunityEvent>> entry in EventRepository.EventsByDate)
                {
                    if (entry.Key < fromDate) continue;
                    if (entry.Key > toDate) break;

                    foreach (CommunityEvent ev in entry.Value)
                    {
                        if (!MatchesKeyword(ev, keyword)) continue;
                        results.Add(ev);
                    }
                }
            }

            results.Sort((a, b) => a.Date.CompareTo(b.Date));
            return results;
        }

        private bool MatchesKeyword(CommunityEvent ev, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return true;

            return ev.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                || ev.Description.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                || ev.Location.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                || ev.Category.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndex = 0;
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today.AddMonths(3);
            txtKeyword.Clear();

            DisplayAllUpcomingEvents();
            RefreshRecommendations();
        }

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearch_Click(sender, EventArgs.Empty);
            }
        }

        private void RefreshRecommendations()
        {
            HashSet<string> exclude = new HashSet<string>();

            if (hasSearched)
            {
                foreach (ListViewItem item in lvEvents.Items)
                {
                    string id = item.Tag as string;
                    if (id != null) exclude.Add(id);
                }
            }

            List<CommunityEvent> recommendations = ComputeRecommendations(exclude);

            lbRecommendations.BeginUpdate();
            lbRecommendations.Items.Clear();

            foreach (CommunityEvent ev in recommendations)
            {
                lbRecommendations.Items.Add(string.Format("{0}  -  {1} ({2:dd MMM})",
                    ev.Title, ev.Category, ev.Date));
            }

            lbRecommendations.EndUpdate();
        }

        private List<CommunityEvent> ComputeRecommendations(HashSet<string> excludeIds)
        {
            List<string> topTerms = EventRepository.SearchFrequency
                .OrderByDescending(kv => kv.Value)
                .Take(5)
                .Select(kv => kv.Key)
                .ToList();

            List<KeyValuePair<CommunityEvent, int>> scored =
                new List<KeyValuePair<CommunityEvent, int>>();

            foreach (CommunityEvent ev in EventRepository.EventsById.Values)
            {
                if (excludeIds.Contains(ev.Id)) continue;
                if (ev.Date.Date < DateTime.Today) continue;

                int score = (int)ev.Priority;     

                for (int i = 0; i < topTerms.Count; i++)
                {
                    string term = topTerms[i];
                    int weight = topTerms.Count - i;     

                    if (ev.Category.Equals(term, StringComparison.OrdinalIgnoreCase))
                        score += 4 * weight;
                    if (ev.Title.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                        score += 3 * weight;
                    if (ev.Description.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                        score += weight;
                    if (ev.Location.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                        score += weight;
                }

                double daysUntil = (ev.Date.Date - DateTime.Today).TotalDays;
                if (daysUntil <= 7) score += 3;
                else if (daysUntil <= 14) score += 2;
                else if (daysUntil <= 30) score += 1;

                scored.Add(new KeyValuePair<CommunityEvent, int>(ev, score));
            }

            return scored
                .OrderByDescending(pair => pair.Value)
                .Take(8)
                .Select(pair => pair.Key)
                .ToList();
        }

        private void DisplaySearchHistory()
        {
            List<string> recent = EventRepository.SearchHistory.Take(5).ToList();

            lblSearchHistory.Text = "Recent searches: " +
                (recent.Count == 0 ? "none yet" : string.Join("  |  ", recent));
        }

        private void lvEvents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvEvents.SelectedItems.Count == 0) return;

            string id = lvEvents.SelectedItems[0].Tag as string;
            if (id == null || !EventRepository.EventsById.ContainsKey(id)) return;

            CommunityEvent ev = EventRepository.EventsById[id];

            EventRepository.IncrementSearchFrequency(ev.Category);
            RefreshRecommendations();

            MessageBox.Show(
                "Category: " + ev.Category +
                "\nDate: " + ev.Date.ToString("dd MMMM yyyy HH:mm") +
                "\nLocation: " + ev.Location +
                "\nPriority: " + ev.Priority +
                "\n\n" + ev.Description,
                ev.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lbRecommendations_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string text = lbRecommendations.SelectedItem as string;
            if (text == null) return;

            CommunityEvent ev = EventRepository.EventsById.Values
                .FirstOrDefault(x => text.StartsWith(x.Title));
            if (ev == null) return;

            EventRepository.IncrementSearchFrequency(ev.Category);
            RefreshRecommendations();

            MessageBox.Show(
                "Category: " + ev.Category +
                "\nDate: " + ev.Date.ToString("dd MMMM yyyy HH:mm") +
                "\nLocation: " + ev.Location +
                "\nPriority: " + ev.Priority +
                "\n\n" + ev.Description,
                ev.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBackToMain_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}