// ======================================



// 

// ======================================

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MCS.Web.ViewModels
{
    public class CardkeyViewModel
    {
        public CardkeyViewModel()
        {
            CardkeyVersion = new Cardkey[10];
        }
        public int StationId { get; set; }
        public string DeviceNo { get; set; }
        public Cardkey[] CardkeyVersion { get; set; }
    }

    public class Cardkey
    {
        public string Key { get; set; }
        public string Version { get; set; }
    }

    public class CardkeyViewModelValidator : AbstractValidator<CardkeyViewModelValidator>
    {
        public CardkeyViewModelValidator()
        {
           
        }
    }
}
