using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurukuru.Classes
{
    internal class ThemeClass
    {
        public static void LightTheme()
        {
            var uriBasicInput = new Uri(".\\Resources\\ResourceDictionaries\\UI\\BasicInput.xaml", UriKind.Relative);
            var uriCommandBar = new Uri(".\\Resources\\ResourceDictionaries\\UI\\CommandBar.xaml", UriKind.Relative);
            var uriGridSplitter = new Uri(".\\Resources\\ResourceDictionaries\\UI\\GridSplitter.xaml", UriKind.Relative);
            var uriLists = new Uri(".\\Resources\\ResourceDictionaries\\UI\\ListsAndCollections.xaml", UriKind.Relative);
            var uriPrimitives = new Uri(".\\Resources\\ResourceDictionaries\\UI\\Primitives.xaml", UriKind.Relative);
            var uriScrolling = new Uri(".\\Resources\\ResourceDictionaries\\UI\\Scrolling.xaml", UriKind.Relative);
            var uriTextFields = new Uri(".\\Resources\\ResourceDictionaries\\UI\\TextFields.xaml", UriKind.Relative);
            var uriTitleBar = new Uri(".\\Resources\\ResourceDictionaries\\UI\\TitleBar.xaml", UriKind.Relative);
            var uriTreeView = new Uri(".\\Resources\\ResourceDictionaries\\UI\\TreeView.xaml", UriKind.Relative);
            var uriWindow = new Uri(".\\Resources\\ResourceDictionaries\\UI\\Window.xaml", UriKind.Relative);
            ResourceDictionary? resourceDict = Application.LoadComponent(uriBasicInput) as ResourceDictionary;
            ResourceDictionary? resourceDict1 = Application.LoadComponent(uriCommandBar) as ResourceDictionary;
            ResourceDictionary? resourceDict2 = Application.LoadComponent(uriGridSplitter) as ResourceDictionary;
            ResourceDictionary? resourceDict3 = Application.LoadComponent(uriLists) as ResourceDictionary;
            ResourceDictionary? resourceDict4 = Application.LoadComponent(uriPrimitives) as ResourceDictionary;
            ResourceDictionary? resourceDict5 = Application.LoadComponent(uriScrolling) as ResourceDictionary;
            ResourceDictionary? resourceDict6 = Application.LoadComponent(uriTextFields) as ResourceDictionary;
            ResourceDictionary? resourceDict7 = Application.LoadComponent(uriTitleBar) as ResourceDictionary;
            ResourceDictionary? resourceDict8 = Application.LoadComponent(uriTreeView) as ResourceDictionary;
            ResourceDictionary? resourceDict9 = Application.LoadComponent(uriWindow) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict1);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict2);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict3);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict4);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict5);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict6);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict7);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict8);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict9);
            File.WriteAllText(".\\Settings\\Theme.txt", "1");
        }
        public static void DarkTheme()
        {
            var uriBasicInput = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkBasicInput.xaml", UriKind.Relative);
            var uriCommandBar = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkCommandBar.xaml", UriKind.Relative);
            var uriGridSplitter = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkGridSplitter.xaml", UriKind.Relative);
            var uriLists = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkListsAndCollections.xaml", UriKind.Relative);
            var uriPrimitives = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkPrimitives.xaml", UriKind.Relative);
            var uriScrolling = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkScrolling.xaml", UriKind.Relative);
            var uriTextFields = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkTextFields.xaml", UriKind.Relative);
            var uriTitleBar = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkTitleBar.xaml", UriKind.Relative);
            var uriTreeView = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkTreeView.xaml", UriKind.Relative);
            var uri9 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkWindow.xaml", UriKind.Relative);
            ResourceDictionary? resourceDict = Application.LoadComponent(uriBasicInput) as ResourceDictionary;
            ResourceDictionary? resourceDict1 = Application.LoadComponent(uriCommandBar) as ResourceDictionary;
            ResourceDictionary? resourceDict2 = Application.LoadComponent(uriGridSplitter) as ResourceDictionary;
            ResourceDictionary? resourceDict3 = Application.LoadComponent(uriLists) as ResourceDictionary;
            ResourceDictionary? resourceDict4 = Application.LoadComponent(uriPrimitives) as ResourceDictionary;
            ResourceDictionary? resourceDict5 = Application.LoadComponent(uriScrolling) as ResourceDictionary;
            ResourceDictionary? resourceDict6 = Application.LoadComponent(uriTextFields) as ResourceDictionary;
            ResourceDictionary? resourceDict7 = Application.LoadComponent(uriTitleBar) as ResourceDictionary;
            ResourceDictionary? resourceDict8 = Application.LoadComponent(uriTreeView) as ResourceDictionary;
            ResourceDictionary? resourceDict9 = Application.LoadComponent(uri9) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict1);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict2);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict3);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict4);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict5);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict6);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict7);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict8);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict9);
            File.WriteAllText(".\\Settings\\Theme.txt", "2");
        }
    }
}
