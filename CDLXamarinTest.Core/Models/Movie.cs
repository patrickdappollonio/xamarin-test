using System;
using System.Collections.Generic;

namespace CDLXamarinTest.Core
{
	public class Movie
	{
		public int ID { get; set; }
		public Uri PosterURL { get; set; }
		public string Title { get; set; }
		public string Overview { get; set; }
		public DateTime ReleaseDate { get; set; }
		public float VoteCount { get; set; }
		public float VoteAverage { get; set; }
	}

	public class MovieBag
	{
		public Movie Movie { get; set; }
		public List<Movie> Related { get; set; }
	}
}

