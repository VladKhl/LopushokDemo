using LopushokDemo.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LopushokDemo
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LopushokDBEntities lopushokDBEntities = new LopushokDBEntities();
        public static Frame MainFrame { get; set; }
    }
}
