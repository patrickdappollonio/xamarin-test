using System;

namespace CDLXamarinTest.Core
{
	public interface ISingleMovieService
	{
		void Get(int ID, Action<MovieBag> success, Action<Exception> error);
	}
}

