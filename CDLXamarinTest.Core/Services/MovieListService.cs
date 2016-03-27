using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace CDLXamarinTest.Core
{
	public class MovieListService : IMovieListService 
	{
		public void Get(Action<List<Movie>> success, Action<Exception> error) 
		{
			var url = string.Format (Constants.ListURL, Constants.APIKey);

			var request = (HttpWebRequest)WebRequest.Create (url);
			try {
				request.BeginGetResponse(result => ProcessResponse(success, error, request, result), null);
			} catch (Exception ex) {
				error (ex);
			}

		}

		private void ProcessResponse(Action<List<Movie>> success, Action<Exception> error, HttpWebRequest request, IAsyncResult result)
		{
			try {
				var response = request.EndGetResponse(result);

				using (var stream = response.GetResponseStream())
				using (var reader = new StreamReader(stream))
				{
					var text = reader.ReadToEnd();
					success(ParseList(text));
				}
			} catch (Exception e) {
				error (e);
			}
		}

		private List<Movie> ParseList(string text)
		{
			var result = JObject.Parse (text);
			var list = new List<Movie> ();

			foreach (var m in result["results"]) {
				var s = new Movie {
					ID = (int)m ["id"],
					PosterURL = new Uri (string.Format (Constants.PosterPath, m ["poster_path"])),
					Title = string.Format ("{0}", m ["original_title"]),
					Overview = string.Format ("{0}", m ["overview"]),
					ReleaseDate = DateTime.Parse ((string)m ["release_date"]), 
					VoteCount = (float)m ["vote_count"],
					VoteAverage = (float)m ["vote_average"] 
				};
						
				list.Add (s);
			}

			return list;
		}
	}
}

