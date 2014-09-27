using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using OxyPlot;
using SharpOptimization.Visual.Annotations;

namespace SharpOptimization.Visual.ViewModels
{
    public class PlotViewModel : INotifyPropertyChanged
    {
        private PlotModel plotModel;

        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
        }

        public PlotViewModel()
        {
            PlotModel = new PlotModel();
            SetUpPlotModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetUpPlotModel()
        {
            PlotModel.LegendTitle = "Legend";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;

            var iterationAxis = new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "Algorithm Iterations",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            };

            PlotModel.Axes.Add(iterationAxis);

            var valueAxis = new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Best Fitness"
            };
            PlotModel.Axes.Add(valueAxis);
        }


        public void UpdateModel()
        {
            
        }
    }
}
