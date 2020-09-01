using System;

namespace SCM.Models
{
	public class Product
	{
		public long ProductId { get; set; } 
		public string Name { get; set; }
		public string SKU { get; set; }
		public decimal Price { get; set; }
		public bool IsMembership { get; set; }
		public bool IsDigital { get; set; }
		public double Weight { get; set; }
		public string WeightUnit { get; set; }
	}
	public class PhysicalProduct : Product
	{
		public string HandlingInstructions { get; set; }
	}

	public class MembershipProduct : Product
	{
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public MemberShipType MemberShipType { get; set; }
	}

	public class DigitalProduct : Product
	{
		public string AccessPath { get; set; }
	}
}
