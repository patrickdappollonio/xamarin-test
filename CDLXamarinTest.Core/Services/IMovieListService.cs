using System;
using System.Collections.Generic;

namespace CDLXamarinTest.Core
{
	public interface IMovieListService 
	{
		void Get(Action<List<Movie>> success, Action<Exception> error);
	}
}

