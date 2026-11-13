using System;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            using (ReportIssuesForm reportForm = new ReportIssuesForm())
            {
                reportForm.ShowDialog();
            }
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            using (EventsForm eventsForm = new EventsForm())
            {
                eventsForm.ShowDialog();
            }
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            using (ServiceStatusForm statusForm = new ServiceStatusForm())
            {
                statusForm.ShowDialog();
            }
        }
    }
}