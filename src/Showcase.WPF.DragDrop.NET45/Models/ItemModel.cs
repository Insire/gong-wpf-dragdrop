using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JetBrains.Annotations;
using Showcase.WPF.DragDrop.ViewModels;
using GongSolutions.Wpf.DragDrop;
using System.Windows;

namespace Showcase.WPF.DragDrop.Models
{
  public class ItemModel : INotifyPropertyChanged
  {
    private double _bindableDoubleValue;
    private string _selectedSubItem;

    public ItemModel()
    {
      this.BindableDoubleValue = Faker.RandomNumber.Next(0, 100);
      for (int i = 0; i < Faker.RandomNumber.Next(2, 20); i++)
      {
        SubItemCollection.Add(new SubItemModel($"Sub item {i}"));
      }
    }

    public ItemModel(int itemIndex) : this()
    {
      this.Index = itemIndex;
      this.Caption = $"Item {itemIndex}";
      this.DropTarget = new ItemModelDropHandler();
    }

    public int Index { get; set; }
    public string Caption { get; set; }
    public IDropTarget DropTarget { get; set; }

    public ObservableCollection<SubItemModel> SubItemCollection { get; set; } = new ObservableCollection<SubItemModel>();

    public string SelectedSubItem
    {
      get { return _selectedSubItem; }
      set
      {
        if (value == _selectedSubItem) return;
        _selectedSubItem = value;
        OnPropertyChanged();
      }
    }

    public double BindableDoubleValue
    {
      get { return _bindableDoubleValue; }
      set
      {
        if (value.Equals(_bindableDoubleValue)) return;
        _bindableDoubleValue = value;
        OnPropertyChanged();
      }
    }

    public override string ToString()
    {
      return this.Caption;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  public class ItemModelDropHandler : IDropTarget
  {
    public void DragOver(IDropInfo dropInfo)
    {
      switch (dropInfo.Data)
      {
        case ItemModel _:
          dropInfo.Effects = DragDropEffects.Move;
          break;

        case SubItemModel _:
          dropInfo.Effects = DragDropEffects.Move;
          break;
      }
    }

    public void Drop(IDropInfo dropInfo)
    {
      switch (dropInfo.Data)
      {
        case ItemModel _:
          MessageBox.Show(typeof(ItemModel).Name + " was dropped in " + typeof(ItemModelDropHandler).Name);
          break;

        case SubItemModel _:
          MessageBox.Show(typeof(SubItemModel).Name + " was dropped in " + typeof(ItemModelDropHandler).Name);
          break;
      }
    }
  }

  public class SubItemModel : INotifyPropertyChanged
  {
    private string _bindableValue;
    private bool _bindableOptionA;
    private bool _bindableOptionB;

    public SubItemModel(string caption)
    {
      this.Caption = caption;
      this.ButtonTestCommand = new SimpleCommand(o => { this.BindableValue = $"Button clicked at {DateTime.UtcNow.ToLocalTime()}"; });
      this.DropTarget = new SubItemDropHandler();
    }

    public IDropTarget DropTarget { get; set; }
    public string Caption { get; set; }

    public ICommand ButtonTestCommand { get; set; }

    public string BindableValue
    {
      get { return _bindableValue; }
      set
      {
        if (value == _bindableValue) return;
        _bindableValue = value;
        OnPropertyChanged();
      }
    }

    public bool BindableOptionA
    {
      get { return _bindableOptionA; }
      set
      {
        if (value == _bindableOptionA) return;
        _bindableOptionA = value;
        OnPropertyChanged();
      }
    }

    public bool BindableOptionB
    {
      get { return _bindableOptionB; }
      set
      {
        if (value == _bindableOptionB) return;
        _bindableOptionB = value;
        OnPropertyChanged();
      }
    }

    public override string ToString()
    {
      return this.Caption;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  public class SubItemDropHandler : IDropTarget
  {
    public void DragOver(IDropInfo dropInfo)
    {
      switch (dropInfo.Data)
      {
        case ItemModel _:
          dropInfo.Effects = DragDropEffects.Link;
          break;

        case SubItemModel _:
          dropInfo.Effects = DragDropEffects.Link;
          break;
      }
    }

    public void Drop(IDropInfo dropInfo)
    {
      switch (dropInfo.Data)
      {
        case ItemModel _:
          MessageBox.Show(typeof(ItemModel).Name + " was dropped in " + typeof(SubItemDropHandler).Name);
          break;

        case SubItemModel _:
          MessageBox.Show(typeof(SubItemModel).Name + " was dropped in " + typeof(SubItemDropHandler).Name);
          break;
      }
    }
  }
}