using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Terminal.Gui;
using NStack;

namespace UICatalog.Scenarios {
	[ScenarioMetadata (Name: "GridView", Description: "Demonstrates a Gridview")]
	[ScenarioCategory ("Controls")]
	class GridViewDemo : Scenario {

		public override void Setup ()
		{
			//TODO: Duplicated code in Demo.cs Consider moving to shared assembly
			var items = new List<ustring> ();
			foreach (var dir in new [] { "/etc", @$"{Environment.GetEnvironmentVariable ("SystemRoot")}\System32" }) {
				if (Directory.Exists (dir)) {
					items = Directory.GetFiles (dir).Union(Directory.GetDirectories(dir))
						.Select (Path.GetFileName)
						.Where (x => char.IsLetterOrDigit (x [0]))
						.OrderBy (x => x).Select(x => ustring.Make(x)).ToList() ;
				}
			}

			// GridView
			var lbGridView = new Label ("GridView") {
				ColorScheme = Colors.TopLevel,
				X = 0,
				Width = Dim.Percent (90)
			};

			var gridView = new GridView (items) {
				X = 0,
				Y = Pos.Bottom (lbGridView) + 1,
				Height = Dim.Fill(2),
				Width = Dim.Percent (90)
			};
			gridView.SelectedItemChanged += (ListViewItemEventArgs e) => lbGridView.Text = items [gridView.SelectedItem];
			Win.Add (lbGridView, gridView);

		
			var btnMoveUp = new Button ("Move _Up") {
				X = 1,
				Y = Pos.Bottom(lbGridView),
			};
			btnMoveUp.Clicked += () => {
				gridView.MoveUp ();
			};

			var btnMoveDown = new Button ("Move _Down") {
				X = Pos.Right (btnMoveUp) + 1,
				Y = Pos.Bottom (lbGridView),
			};
			btnMoveDown.Clicked += () => {
				gridView.MoveDown ();
			};

			Win.Add (btnMoveUp, btnMoveDown);
		}
	}
}
