using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace CDLXamarinTest.Core
{
	public class SingleMovieService : ISingleMovieService
	{
		public void Get(int ID, Action<MovieBag> success, Action<Exception> error) 
		{
			var _movieurl = string.Format (Constants.MovieURL, ID, Constants.APIKey);
			var _relatedurl = string.Format (Constants.SimilarURL, ID, Constants.APIKey);

			var process = new[]{ _movieurl, _relatedurl };
			var tasks = process.Select (GetAsync).ToArray ();

			var completed = Task.Factory.ContinueWhenAll (tasks, completedTasks => {
				string movieinfo = "", relatedinfo = "";
				Exception ex = null;

				for (var i = 0; i < completedTasks.Length; i++) {
					var t = completedTasks[i];

					if (t.Status == TaskStatus.RanToCompletion) {
						if (i == 0) movieinfo = t.Result;
						else relatedinfo = t.Result;
					}

					if (t.Status == TaskStatus.Faulted) {
						ex = t.Exception;
						break;
					}
				}

				if (ex != null) error(ex);

				var movie = ProcessMovie(movieinfo);
				var related = ProcessRelated(relatedinfo);

				success(new MovieBag{ Movie = movie, Related = related });
			});

		}

		private Movie ProcessMovie(string text) 
		{
			var result = JObject.Parse (text);
			return new Movie {
				ID = (int)result ["id"],
				PosterURL = new Uri (string.Format (Constants.PosterPath, result ["poster_path"])),
				Title = string.Format ("{0}", result ["original_title"]),
				Overview = string.Format ("{0}", result ["overview"]),
				ReleaseDate = DateTime.Parse ((string)result ["release_date"]),
				VoteCount = (float)result ["vote_count"],
				VoteAverage = (float)result ["vote_average"] 
			};
		}

		private List<Movie> ProcessRelated(string text)
		{
			var result = JObject.Parse (text);
			var list = new List<Movie> ();


			foreach (var m in result["results"]) {
				if (list.Count == 3)
					break;

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

		private Task<string> GetAsync(string url)
		{
			var tcs = new TaskCompletionSource<string>();
			var request = (HttpWebRequest)WebRequest.Create(url);
			try
			{
				request.BeginGetResponse(ss => {
					HttpWebResponse response = null;
					try {
						response = (HttpWebResponse)request.EndGetResponse(ss);
						using(var reader = new StreamReader(response.GetResponseStream()))
						{
							tcs.SetResult(reader.ReadToEnd());
						}                    
					}
					catch(Exception exc) { tcs.SetException(exc); }
				}, null);
			}
			catch(Exception exc) { tcs.SetException(exc); }
			return tcs.Task;
		}
	}
}

