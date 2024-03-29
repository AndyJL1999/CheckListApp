﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CheckListApi.DTOs;
using System.Configuration;
using CheckListWPF.Resources.Interfaces;
using CheckListWPF.MVVM.Model;
using CheckListApi.DTOs.PutDtos;

namespace CheckListWPF.Resources.Helpers
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;
        private ILoggedInUser _loggedInUser;
        private string _password;

        public ApiHelper(ILoggedInUser loggedInUser)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;
        }

        public ILoggedInUser LoggedInUser
        {
            get { return _loggedInUser; }
            set
            {
                _loggedInUser = value;
            }
        }
        public HttpClient ApiClient
        {
            get { return _apiClient; }
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["ApiUrl"];

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string email, string password)
        {
            var data = JsonContent.Create(new LoginDto
            {
                Email = email,
                Password = password
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync(_apiClient.BaseAddress + "Auth/login", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthenticatedUser>();

                    _password = password;

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<string> Register(string username, string password, string email)
        {
            var data = JsonContent.Create(new RegisterDto
            {
                Username = username,
                Password = password,
                Email = email
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync(_apiClient.BaseAddress + "Auth/register", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task GetUserInfo(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await _apiClient.GetAsync(_apiClient.BaseAddress + "User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoggedInUser>();
                    _loggedInUser.Username = result.Username;
                    _loggedInUser.Email = result.Email;
                    _loggedInUser.BackgroundColor = result.BackgroundColor;
                    _loggedInUser.Id = result.Id;
                    _loggedInUser.Token = token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<string> UpdateUser(string username, string email, string backgroundColor)
        {
            var data = JsonContent.Create(new UpdateUserDto
            {
                Username = username,
                Password = _password,
                Email = email,
                BackgroundColor = backgroundColor
            });

            using (HttpResponseMessage response = await _apiClient.PutAsync(_apiClient.BaseAddress + "User", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    _loggedInUser.Username = username;
                    _loggedInUser.Email = email;
                    _loggedInUser.BackgroundColor = backgroundColor;

                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<string> UpdatePassword(string oldPassword, string newPassword)
        {
            if (oldPassword == _password)
            {
                var data = JsonContent.Create(new UpdateUserDto
                {
                    Username = _loggedInUser.Username,
                    Password = newPassword,
                    Email = _loggedInUser.Email,
                    BackgroundColor = _loggedInUser.BackgroundColor
                });

                using (HttpResponseMessage response = await _apiClient.PutAsync(_apiClient.BaseAddress + "User", data))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            else
            {
                throw new Exception("Incorrect Password");
            }
            
        }
    }
}
