using System;

namespace GameStoreMobileApp
{
	public class Game
	{
		public int GameId {get;set;}
		public string GameName { get; set; }
		public DateTime ReleaseDate { get; set;}
		public decimal Price { get; set;}
		public int InventoryStock { get; set;}
		public bool check { get; set;}
	}
}

