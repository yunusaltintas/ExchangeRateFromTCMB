using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CurrencyRate.Application.Dtos
{
	[XmlRoot(ElementName = "Tarih_Date")]
	public class CurrencyListDto
	{
		[XmlElement(ElementName = "Currency")]
		public List<Currency> Currency { get; set; }

		[XmlAttribute(AttributeName = "Tarih")]
		public string DataTarihString { get; set; }

		[XmlAttribute(AttributeName = "Date")]
		public string DataDateString { get; set; }

		[XmlAttribute(AttributeName = "Bulten_No")]
		public string BultenNumber { get; set; }
	}

	[XmlRoot(ElementName = "Currency")]
	public class Currency
	{
		[XmlElement(ElementName = "Unit")]
		public string Unit { get; set; }

		[XmlElement(ElementName = "Isim")]
		public string Isim { get; set; }

		[XmlElement(ElementName = "CurrencyName")]
		public string CurrencyName { get; set; }

		[XmlElement(ElementName = "ForexBuying")]
		public decimal ForexBuying { get; set; }

		[XmlElement(ElementName = "ForexSelling")]
		public string? ForexSelling { get; set; }

		[XmlElement(ElementName = "BanknoteBuying")]
		public string? BanknoteBuying { get; set; }

		[XmlElement(ElementName = "BanknoteSelling")]
		public string? BanknoteSelling { get; set; }

		[XmlElement(ElementName = "CrossRateUSD")]
		public string? CrossRateUSD { get; set; }

		[XmlElement(ElementName = "CrossRateOther")]
		public string? CrossRateOther { get; set; }

		[XmlAttribute(AttributeName = "CrossOrder")]
		public string CrossOrder { get; set; }

		[XmlAttribute(AttributeName = "Kod")]
		public string Kod { get; set; }

		[XmlAttribute(AttributeName = "CurrencyCode")]
		public string CurrencyCode { get; set; }
	}
}
