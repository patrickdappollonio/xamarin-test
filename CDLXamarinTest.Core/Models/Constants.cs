using System;

namespace CDLXamarinTest.Core
{
	public static class Constants 
	{
		public const string APIKey = "ab41356b33d100ec61e6c098ecc92140";
		public const string ListURL = "http://api.themoviedb.org/3/discover/movie?api_key={0}&sort_by=popularity.desc";
		public const string SimilarURL = "http://api.themoviedb.org/3/movie/{0}/similar?api_key={1}";
		public const string MovieURL = "http://api.themoviedb.org/3/movie/{0}?api_key={1}";

		public const string BackdropPath = "http://image.tmdb.org/t/p/w300{0}";
		public const string PosterPath = "https://image.tmdb.org/t/p/w342{0}";

	}
}

