using System;

namespace SCM.Models
{
	public class Product
	{
		public long ProductId { get; set; } 
		public string Name { get; set; }
		public string SKU { get; set; }
		public decimal Price { get; set; }
		public double Weight { get; set; }
		public string WeightUnit { get; set; }
	}
	public class PhysicalProduct : Product
	{
		public string HandlingInstructions { get; set; }
	}

	public class MemberShipProduct : Product
	{
		public MemberShipType MemberShipType { get; set; }
	}

	public class DigitalProduct : Product
	{
		public string AccessPath { get; set; }
	}
}
