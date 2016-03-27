using System;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;

namespace CDLXamarinTest.Core.ViewModels
{
	public class ListViewModel : MvxViewModel
	{
		private readonly IMovieListService _service;

		public ListViewModel(IMovieListService service)
		{
			_service = service;
		}

		public override void Start()
		{
			IsLoading = true;
			_service.Get (OnMoviesFound, OnError);
		}

		private void OnMoviesFound(List<Movie> list) 
		{
			IsLoading = false;
			Items = list;
		}

		private void OnError(Exception e)
		{
			IsLoading = false;
			// TODO imprimir mensaje
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

		private List<Movie> _items;

		public List<Movie> Items {
			get { return _items; }
			set {
				_items = value;
				RaisePropertyChanged (() => Items);
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

