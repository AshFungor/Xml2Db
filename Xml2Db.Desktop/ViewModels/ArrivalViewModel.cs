using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xml2Db.Models;

namespace Xml2Db.Desktop.ViewModels
{
    public class ArrivalViewModel
    {
        private readonly Func<Context> _contextFactory;

        public ArrivalViewModel(Func<Context> contextFactory)
        {
            _contextFactory = contextFactory;
            Load();
        }

        public ObservableCollection<Reference> Items { get; } = new ObservableCollection<Reference>();

        public void Load()
        {
            using (var context = _contextFactory())
            {
                Items.Clear();
                Items.AddRange(context.Arrivals);
            }
        }
    }
}
