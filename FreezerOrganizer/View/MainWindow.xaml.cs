﻿using System;
using System.Collections.Generic;
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
using FreezerOrganizer.ViewModel;

namespace FreezerOrganizer.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int spaceBelowAndAbove = 100;

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight - spaceBelowAndAbove;
        }
    }
}
