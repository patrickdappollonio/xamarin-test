using System;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;

namespace CDLXamarinTest.Core
{
	public class SingleViewModel : MvxViewModel
	{
		private readonly ISingleMovieService _service;

		public SingleViewModel(ISingleMovieService service)
		{
			_service = service;
		}

		public void Init(Movie m)
		{
			Bag = new MovieBag();

			Bag.Movie = m;
			Bag.Related = new List<Movie>{
				new Movie(),
				new Movie(),
				new Movie()
			};
		}

		public override void Start()
		{
			IsLoading = true;
			_service.Get (Bag.Movie.ID, OnDataFound, OnError);
		}

		private void OnDataFound(MovieBag bag) 
		{
			IsLoading = false;
			Bag = bag;
		}

		private void OnError(Exception e)
		{
			IsLoading = false;
			System.Diagnostics.Debug.WriteLine ("Error: {0}", e.Message);
		}

		public void PlayVideo(Movie m)
		{

		}

		public void SaveFavorite(Movie m)
		{

		}

		private void ItemClick(Movie m)
		{
			ShowViewModel<SingleViewModel>(m);
		}

		#region Reactive

		private bool _isLoading;

		public bool IsLoading {
			get { return _isLoading; }
			set {
				_isLoading = value;
				RaisePropertyChanged (() => IsLoading);
			}
		}

		private MovieBag _bag;

		public MovieBag Bag {
			get { return _bag; }
			set {
				_bag = value;
				RaisePropertyChanged (() => Bag);
			}
		}

		private MvxCommand<Movie> _playVideoCommand;

		public System.Windows.Input.ICommand PlayVideoCommand {
			get {
				_playVideoCommand = _playVideoCommand ?? new MvxCommand<Movie> (PlayVideo);
				return _playVideoCommand;
			}
		}

		private MvxCommand<Movie> _saveToFavoritesCommand;

		public System.Windows.Input.ICommand SaveToFavoritesCommand {
			get {
				_saveToFavoritesCommand = _saveToFavoritesCommand ?? new MvxCommand<Movie> (SaveFavorite);
				return _saveToFavoritesCommand;
			}
		}

		private MvxCommand<Movie> _itemSelectedCommand;

		public System.Windows.Input.ICommand ItemSelectedCommand {
			get {
				_itemSelectedCommand = _itemSelectedCommand ?? new MvxCommand<Movie> (ItemClick);
				return _itemSelectedCommand;
			}
		}

		#endregion
	}
}

