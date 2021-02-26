using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xml2Db.Models;

namespace Xml2Db.Desktop.ViewModels
{
    public class SellsViewModel
    {
        private readonly Func<Context> _contextFactory;

        public SellsViewModel(Func<Context> contextFactory)
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
                Items.AddRange(context.Sells);
            }
        }
    }
}
