﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microcharts;
using SkiaSharp;
using ClearData.Models;
using System.Collections.Generic;

namespace ClearData.ViewModels
{
    /**
     * view model for the about/home page of the app. Handles the creation of the graph being shown on that page.
     */
    public class AboutViewModel : BaseViewModel
    {
        public enum TimePeriod { ALL_TIME = 0, MONTHLY = 1, WEEKLY = 2 }
        public enum DisplayType { COMPANIES = 0, DATATYPES = 1 }

        //an array of colours to use for the graphs being made
        private static SKColor[] Colors = {SKColor.Parse("#266489"), SKColor.Parse("#68B9C0"), SKColor.Parse("#90D585"), SKColor.Parse("#F3C151"),
                                           SKColor.Parse("#F37F64"), SKColor.Parse("#424856"), SKColor.Parse("#424856"), SKColor.Parse("#8F97A4"),
                                           SKColor.Parse("#76846E"), SKColor.Parse("#A65B69"), SKColor.Parse("#DABFAF"), SKColor.Parse("#97A69D")};

        public AboutViewModel()
        {
            Period = (int)TimePeriod.ALL_TIME;
            Display = (int)DisplayType.COMPANIES;
            DisplayPrice = "";
            UpdateDonutChart(); //set up the donut chart for the first time
        }

        /**
         * Updates the donut chart with a) the database b) the settings/filters which have been applied. Also updates the price being displayed
         */
        public async void UpdateDonutChart()
        {
            if (UserInfo.GetPermissions() == null)
            {
                return;
            }
            //get all the logs, this does a database call
            List<BasicLog> logList = await UserInfo.GetPermissions().RetrieveAllLogsList();

            //work out the price associated with each datatype by creating a dictionary which maps ids to their price
            Dictionary<int, double> profits = new Dictionary<int, double>();
            double totalProfit = 0;
            foreach (BasicLog basicLog in logList)
            {
                //check the time to see if its within the range we are talking about
                if ((Period == (int)TimePeriod.MONTHLY && basicLog.time < DateTime.Now - new TimeSpan(30*24,0,0)) ||
                     (Period == (int)TimePeriod.WEEKLY && basicLog.time < DateTime.Now - new TimeSpan(7 * 24, 0, 0)))
                {
                    continue;
                }
                totalProfit += basicLog.price;
                //then work out the ID that we are concerned with
                int id;
                if (Display == (int)DisplayType.COMPANIES)
                {
                    id = basicLog.enterprise;
                } else
                {
                    id = basicLog.data_type;
                }
                //then add the value to the profits dictionary
                if (!profits.TryGetValue(id, out double result))
                {
                    profits[id] = 0;
                }
                profits[id] += basicLog.price;
            }

            //now we have all the profits for each id, lets create the entries for the donut chart
            var entryList = new List<ChartEntry>();
            
            if (Display == (int)DisplayType.COMPANIES)
            {
                foreach (Company company in UserInfo.GetPermissions().companies)
                {
                    if (profits.TryGetValue(company.Id, out double result))
                    {
                        entryList.Add(new ChartEntry((float)profits[company.Id]) { Label = company.Name, ValueLabel = String.Format("${0:0.00}", profits[company.Id]),
                                                                                   Color = Colors[Math.Min(Colors.Length, entryList.Count)]});
                    }
                }
            } else
            {
                foreach (DataType dataType in UserInfo.GetPermissions().dataTypes)
                {
                    if (profits.TryGetValue(dataType.Id, out double result))
                    {
                        entryList.Add(new ChartEntry((float)profits[dataType.Id]) { Label = dataType.Name, ValueLabel = String.Format("${0:0.00}", profits[dataType.Id]),
                                                                                    Color = Colors[Math.Min(Colors.Length, entryList.Count)]});
                    }
                }
            }
            //update the donut chart, by creating a new one, we get the nice animation
            DonutChart = new DonutChart() { BackgroundColor = SKColors.Transparent, Entries = entryList.ToArray(), 
                                            LabelTextSize = 30, LabelMode = LabelMode.RightOnly, GraphPosition = GraphPosition.AutoFill };
            DisplayPrice = String.Format("${0:0.00}", totalProfit); //update the displayed price
        }

        private String displayPrice; //string for the price being displayed
        public String DisplayPrice
        {
            get => displayPrice;
            set => SetProperty(ref displayPrice, value);
        }

        //these two are ints not the enums themselves because of interacting with a picker
        private int period;
        public int Period {
            get => period;
            set => SetProperty(ref period, value);
        }

        private int display;
        public int Display {
            get => display;
            set => SetProperty(ref display, value);
        }

        private Chart donutChart;
        public Chart DonutChart
        {
            get => donutChart;
            set => SetProperty(ref donutChart, value);
        }
    }
}