﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login());
            Application.ApplicationExit += Application_ApplicationExit;


        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (GameManger.connStatues)
            {

                try
                {
                    GameManger.SendServerRequest(Flag.disconnect, "");
                }
                catch (Exception)
                {

                   
                }
                
            }
        }
    }
}
