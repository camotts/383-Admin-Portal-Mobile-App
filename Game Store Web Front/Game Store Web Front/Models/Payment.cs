using Game_Store_Web_Front.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Store_Web_Front.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [CardValidator(AcceptedCardTypes = CardValidator.CardType.Visa | CardValidator.CardType.MasterCard)]
        public string CardNum { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Expiration { get; set; }
        public decimal Ammount { get; set; }
    }
}
