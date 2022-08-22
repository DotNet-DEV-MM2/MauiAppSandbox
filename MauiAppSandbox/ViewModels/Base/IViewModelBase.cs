using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.ViewModels.Base
{
    internal interface IViewModelBase : IQueryAttributable
    {
        public bool IsBusy { get; }

        public bool IsNotBusy { get;  }

        public bool IsInitialized { get; set; }

        Task InitializeAsync();
    }
}
