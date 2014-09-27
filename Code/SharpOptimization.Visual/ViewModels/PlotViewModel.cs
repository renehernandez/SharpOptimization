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
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        

    }
}
