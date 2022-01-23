﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using KidsVaccineReminder.BackgroundThread;
namespace KidsVaccineReminder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        BackgroundSendMessage bgThread = new BackgroundSendMessage();
        App()
        {
            bgThread.Start();
        }
    }
}
