using System;
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public enum EventPriority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }

    public class CommunityEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public EventPriority Priority { get; set; }
        public bool IsAnnouncement { get; set; }
    }

    public class PriorityQueue<T>
    {
        private class Node
        {
            public T Item;
            public int Priority;
        }

        private readonly List<Node> _heap = new List<Node>();

        public int Count { get { return _heap.Count; } }
        public bool IsEmpty { get { return _heap.Count == 0; } }

        public void Enqueue(T item, int priority)
        {
            Node node = new Node { Item = item, Priority = priority };
            _heap.Add(node);

            int index = _heap.Count - 1;
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (_heap[parent].Priority >= _heap[index].Priority) break;

                Node temp = _heap[parent];
                _heap[parent] = _heap[index];
                _heap[index] = temp;
                index = parent;
            }
        }

        public T Dequeue()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("The priority queue is empty.");

            T result = _heap[0].Item;

            _heap[0] = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);

            int index = 0;
            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int largest = index;

                if (left < _heap.Count && _heap[left].Priority > _heap[largest].Priority)
                    largest = left;
                if (right < _heap.Count && _heap[right].Priority > _heap[largest].Priority)
                    largest = right;
                if (largest == index) break;

                Node temp = _heap[largest];
                _heap[largest] = _heap[index];
                _heap[index] = temp;
                index = largest;
            }

            return result;
        }
    }

    public static class EventRepository
    {
        public static readonly SortedDictionary<DateTime, List<CommunityEvent>> EventsByDate =
            new SortedDictionary<DateTime, List<CommunityEvent>>();

        public static readonly Dictionary<string, List<CommunityEvent>> EventsByCategory =
            new Dictionary<string, List<CommunityEvent>>(StringComparer.OrdinalIgnoreCase);

        public static readonly Dictionary<string, CommunityEvent> EventsById =
            new Dictionary<string, CommunityEvent>();

        public static readonly HashSet<string> UniqueCategories =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public static readonly SortedSet<DateTime> UniqueDates =
            new SortedSet<DateTime>();

        public static readonly Queue<CommunityEvent> AnnouncementQueue =
            new Queue<CommunityEvent>();

        public static readonly Stack<string> SearchHistory =
            new Stack<string>();

        public static readonly PriorityQueue<CommunityEvent> PriorityEvents =
            new PriorityQueue<CommunityEvent>();

        public static readonly Dictionary<string, int> SearchFrequency =
            new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public static void AddEvent(CommunityEvent ev)
        {
            if (ev == null || EventsById.ContainsKey(ev.Id)) return;

            EventsById[ev.Id] = ev;

            DateTime day = ev.Date.Date;

            if (!EventsByDate.ContainsKey(day))
                EventsByDate[day] = new List<CommunityEvent>();
            EventsByDate[day].Add(ev);

            if (!EventsByCategory.ContainsKey(ev.Category))
                EventsByCategory[ev.Category] = new List<CommunityEvent>();
            EventsByCategory[ev.Category].Add(ev);

            UniqueCategories.Add(ev.Category);
            UniqueDates.Add(day);

            if (ev.IsAnnouncement)
                AnnouncementQueue.Enqueue(ev);

            PriorityEvents.Enqueue(ev, (int)ev.Priority);
        }

        public static void IncrementSearchFrequency(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return;

            term = term.Trim().ToLower();
            if (term.Length < 3) return;

            if (SearchFrequency.ContainsKey(term))
                SearchFrequency[term]++;
            else
                SearchFrequency[term] = 1;
        }
    }
}