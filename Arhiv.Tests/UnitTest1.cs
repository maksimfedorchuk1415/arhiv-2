using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Arhiv.Entity;
using Arhiv.Pages;
using Arhiv.Tests;
using Arhiv.Class;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace Arhiv.Tests
{
    [TestClass]
    public class UnitTest1
    {
     

            public class MainWindowTests
            {
                private MainWindow _window;

                [SetUp]
                public void Setup()
                {
                    _window = new MainWindow();
                }

                [Test]
                public void TestMainWindowTitle()
                {
                    string expectedTitle = "My WPF Application";
                    Assert.AreEqual(expectedTitle, _window.Title);
                }

                [Test]
                public void TestButtonClick()
                {
                    Button button = _window.FindName("MyButton") as Button;

                    button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));

                    // Add assertions here to test the result of the button click
                }

                [TearDown]
                public void TearDown()
                {
                    _window.Close();
                }
            }
        }
    }
        

