using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using TimerTab.Command;

namespace TimerTab.ViewModel
{
    public class MainViewModel
    {
        public ICommand AddTabCommand { get; set; }
        private readonly ObservableCollection<Tab> _tabs;
        public ICollection<Tab> Tabs { get; }
        public MainViewModel()
        {
            AddTabCommand = new ActionCommand(p => AddTab());
            _tabs = new ObservableCollection<Tab>();
            _tabs.CollectionChanged += Tabs_CollectionChanged;
            Tabs = _tabs;
            AddTab();
        }

        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Tab tab;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    tab = (Tab)e.NewItems[0];
                    tab.CloseRequested += OnTabCloseRequested;
                    break;
                case NotifyCollectionChangedAction.Remove:                   
                    tab = (Tab)e.OldItems[0];
                    tab.CloseRequested -= OnTabCloseRequested;
                    break;
            }
        }
        private void OnTabCloseRequested(object sender, EventArgs e)
        {
            if (Tabs.Count != 1)
            {
                var t = (TabContentViewModel)sender;
                if (!t.IsTimerStart)
                {
                    Tabs.Remove((Tab)sender);
                }
            }
        }
        private void AddTab()
        {
            if(Tabs.Count < 10)
                Tabs.Add(new TabContentViewModel()
                {
                    Name = $"{DateTime.Now.ToString("HH:mm:ss")}  |  {Tabs.Count}"
                } );
        }
    }
    
    public class Tab 
    {
        public Tab()
        {
            CloseCommand = new ActionCommand(p => CloseRequested?.Invoke(this, EventArgs.Empty));
        }
        public string Name { get; set; }
        public ICommand CloseCommand { get; }
        public event EventHandler CloseRequested;
    }

   
}
