using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.XtraScheduler;
using DXOrganizer.Models;

namespace DXOrganizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        DatabaseContext context = new DatabaseContext();
        public MainWindow(DatabaseContext databaseContext)
        {
            InitializeComponent();}
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.MyAppointments.Load();
            context.MyResources.Load();

            this.scheduler.Storage.AppointmentStorage.DataContext = context.MyAppointments.Local;
            this.scheduler.Storage.ResourceStorage.DataContext = context.MyResources.Local;


            this.scheduler.Storage.AppointmentsInserted +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);
            this.scheduler.Storage.AppointmentsChanged +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);
            this.scheduler.Storage.AppointmentsDeleted +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);
        }

        void Storage_AppointmentsModified(object sender, PersistentObjectsEventArgs e)
        {
            context.SaveChanges();
            context.Dispose();
        }}
}
