using CheckListApi.Models;
using CheckListWPF.Resources;
using CheckListWPF.Resources.EventAggregators;
using CheckListWPF.Resources.Helpers;
using CheckListWPF.Resources.Interfaces;
using Prism.Events;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CheckListWPF.MVVM.ViewModel.ActionViewModels
{
    public class EditAccountViewModel : ActionViewModel
    {
        #region ----------Fields----------
        private readonly IApiHelper _apiHelper;
        private readonly IEventAggregator _eventAggregator;
        private ICommand _editAccountCommand;
        private ICommand _showHiddenFormCommand;
        private Visibility _changePasswordVisibility;
        private Visibility _editAccountVisibility;
        private string _username;
        private string _email;
        private string _oldPassword;
        private string _newPassword;
        private string _confirmPassword;
        private bool _onEditAccountView;
        #endregion

        public EditAccountViewModel(IApiHelper apiHelper, IEventAggregator eventAggregator)
        {
            _apiHelper = apiHelper;
            _eventAggregator = eventAggregator;
            _onEditAccountView = true;

            ChangePasswordVisibility = Visibility.Collapsed;
            EditAccountVisibility = Visibility.Visible;
            ErrorVisibility = Visibility.Collapsed;

            Username = _apiHelper.LoggedInUser.Username;
            Email = _apiHelper.LoggedInUser.Email;
        }

        #region ----------Properties----------
        public string Username 
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        
        public string OldPassword 
        {
            get { return _oldPassword; }
            set
            {
                _oldPassword = value;
                OnPropertyChanged(nameof(OldPassword));
            }
        }
       
        public string NewPassword 
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }
       
        public string ConfirmPassword 
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        
        public string Email 
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public Visibility EditAccountVisibility 
        {
            get { return _editAccountVisibility; }
            set
            {
                _editAccountVisibility = value;
                OnPropertyChanged(nameof(EditAccountVisibility));
            } 
        }

        public Visibility ChangePasswordVisibility 
        {
            get { return _changePasswordVisibility; }
            set
            {
                _changePasswordVisibility = value;
                OnPropertyChanged(nameof(ChangePasswordVisibility));
            }
        }


        public ICommand EditAccountCommand
        {
            get
            {
                if (_editAccountCommand is null)
                {
                    _editAccountCommand = new RelayCommand(p => EditAccount(), p => true);
                }

                return _editAccountCommand;
            }
        }

        public ICommand ShowHiddenFormCommand 
        {
            get
            {
                if(_showHiddenFormCommand is null)
                {
                    _showHiddenFormCommand = new RelayCommand(p => ShowForm(), p => true);
                }

                return _showHiddenFormCommand;
            }
        }
        #endregion

        #region ----------Methods----------
        private async void EditAccount()
        {
            try
            {
                if (_onEditAccountView)
                {
                    ErrorVisibility = Visibility.Collapsed;

                    await _apiHelper.UpdateUser(Username, Email);

                    _eventAggregator.GetEvent<UpdateAccountEvent>().Publish() ;

                    CloseWindow();
                }
                else
                {
                    if (NewPassword == ConfirmPassword)
                    {
                        ErrorVisibility = Visibility.Collapsed;

                        await _apiHelper.UpdatePassword(OldPassword, NewPassword);
                    }
                    else
                    {
                        throw new Exception("Confirm password is incorrect");
                    }

                    CloseWindow();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.Trim('"');
                ErrorVisibility = Visibility.Visible;
            }
            
        }

        private void ShowForm()
        {
            _onEditAccountView = !_onEditAccountView;

            if (_onEditAccountView)
            {
                ChangePasswordVisibility = Visibility.Collapsed;
                EditAccountVisibility = Visibility.Visible;
                ErrorVisibility = Visibility.Collapsed;
                ErrorMessage = string.Empty;
                OldPassword = string.Empty;
                NewPassword = string.Empty;
                ConfirmPassword = string.Empty;
            }
            else
            {
                ChangePasswordVisibility = Visibility.Visible;
                EditAccountVisibility = Visibility.Collapsed;
                ErrorVisibility= Visibility.Collapsed;
                ErrorMessage = string.Empty;
                OldPassword = string.Empty;
                NewPassword = string.Empty;
                ConfirmPassword = string.Empty;
            }

        }
        #endregion
    }
}
