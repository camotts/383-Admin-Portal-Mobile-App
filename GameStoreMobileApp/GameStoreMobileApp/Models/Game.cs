using System;
using System.Collections.Generic;

namespace GameStoreMobileApp
{
	public class Game
	{
		public int GameId {get;set;}
		public string URL { get; set;}
		public string GameName { get; set; }
		public DateTime ReleaseDate { get; set;}
		public decimal Price { get; set;}
		public int InventoryStock { get; set;}
		public List<Genre> Genres { get; set;}
		public List<Tag> Tags { get; set;}

		public string GameDescription
		{
			get { return String.Format ("Game: {0}, Price: {1}", GameName, Price); }
		}
	}


}

