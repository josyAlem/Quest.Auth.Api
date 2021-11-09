using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quest.Auth.Common.Request;
using Quest.Auth.Common.Response;
using Quest.Auth.Common.Settings;
using Quest.Auth.Services.Interfaces;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Auth.Services
{
    public class Auth0Service:IAuth0Service
    {
        private RestClient _client;
        private readonly Auth0Settings _auth0Settings;
        public Auth0Service(IOptions<Auth0Settings> auth0Config)
        {
            _auth0Settings = auth0Config.Value;
            _client = new RestClient(_auth0Settings.Domain);
        }
        public async Task<Auth0LoginResponse> Login(Auth0LoginRequest loginRequest)
        {
            var req = new RestRequest(_auth0Settings.Paths.Token, Method.POST);
            req.AddParameter("client_id", loginRequest.ClientId, ParameterType.GetOrPost);
            req.AddParameter("grant_type", loginRequest.GrantType, ParameterType.GetOrPost);
            req.AddParameter("audience", loginRequest.Audience, ParameterType.GetOrPost);
            req.AddParameter("scope", loginRequest.Scope, ParameterType.GetOrPost);
            req.AddParameter("username", loginRequest.UserName, ParameterType.GetOrPost);
            req.AddParameter("password", loginRequest.Password, ParameterType.GetOrPost);
            req.AddParameter("realm", loginRequest.Realm, ParameterType.GetOrPost);
            IRestResponse response = await _client.ExecuteAsync(req);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Log.Error("Login:Access Token cannot be obtained");
                throw new KeyNotFoundException();
            }

            var loginResponse = JsonConvert.DeserializeObject<Auth0LoginResponse>(response.Content);
            return loginResponse;
        }

        public async Task<Auth0LoginResponse> Refresh(Auth0RefreshTokenRequest refreshTokenRequest)
        {
            var req = new RestRequest(_auth0Settings.Paths.Token, Method.POST);
            req.AddParameter("client_id", refreshTokenRequest.ClientId, ParameterType.GetOrPost);
            req.AddParameter("grant_type", refreshTokenRequest.GrantType, ParameterType.GetOrPost);
            req.AddParameter("refresh_token", refreshTokenRequest.RefreshToken, ParameterType.GetOrPost);
            IRestResponse response = await _client.ExecuteAsync(req);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Log.Error("Refresh:Access Token cannot be obtained");
                throw new KeyNotFoundException();
            }

            var loginResponse = JsonConvert.DeserializeObject<Auth0LoginResponse>(response.Content);
            return loginResponse;
        }

        public async Task<SignupResponse> SignUp(Auth0SignupRequest signupRequest)
        {
            var getTokenResponse = await GetManagementAPIToken(_auth0Settings.Domain + _auth0Settings.ManagementAPI.Token.Path,
                _auth0Settings.QuestAuth.ClientId, _auth0Settings.QuestAuth.ClientSecret, _auth0Settings.GrantTypes.Client);

            var req = new RestRequest(_auth0Settings.ManagementAPI.Token.Path+_auth0Settings.ManagementAPI.Signup.Path, Method.POST);
            req.AddHeader("authorization", "Bearer "+ getTokenResponse.AccessToken);
            req.AddParameter("email", signupRequest.Email, ParameterType.GetOrPost);
            req.AddParameter("password", signupRequest.Password, ParameterType.GetOrPost);
            req.AddParameter("connection", signupRequest.Connection, ParameterType.GetOrPost);
            IRestResponse response = await _client.ExecuteAsync(req);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Log.Error("Access Token cannot be obtained, process terminate");
                throw new KeyNotFoundException();
            }
            var signupResponse = JsonConvert.DeserializeObject<SignupResponse>(response.Content);
            return signupResponse;
        }

        public async Task<List<Auth0RoleResponse>> GetRoles(Auth0SignupRequest signupRequest)
        {
            var getTokenResponse = await GetManagementAPIToken(_auth0Settings.Domain + _auth0Settings.ManagementAPI.Token.Path,
                _auth0Settings.QuestAuth.ClientId, _auth0Settings.QuestAuth.ClientSecret, _auth0Settings.GrantTypes.Client);

            var req = new RestRequest(_auth0Settings.ManagementAPI.Token.Path+_auth0Settings.ManagementAPI.Roles.Path, Method.GET);
            req.AddHeader("authorization", "Bearer "+ getTokenResponse.AccessToken);
            IRestResponse response = await _client.ExecuteAsync(req);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Log.Error("Access Token cannot be obtained, process terminate");
                throw new KeyNotFoundException();
            }
            var roleResponse = JsonConvert.DeserializeObject<List<Auth0RoleResponse>>(response.Content);
            return roleResponse;
        }

        private async Task<Auth0TokenResponse> GetManagementAPIToken( string audience,string clientId,string clientSecret,string grantType) {

            var req = new RestRequest(_auth0Settings.Paths.Token, Method.POST);
            req.AddParameter("client_id", clientId, ParameterType.GetOrPost);
            req.AddParameter("grant_type", grantType, ParameterType.GetOrPost);
            req.AddParameter("client_secret", clientSecret, ParameterType.GetOrPost);
            req.AddParameter("audience", audience, ParameterType.GetOrPost);
            IRestResponse response =await _client.ExecuteAsync(req);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Log.Error("Access Token cannot be obtained");
                throw new KeyNotFoundException();
            }

            var tokenResponse = JsonConvert.DeserializeObject<Auth0TokenResponse>(response.Content);
            return tokenResponse;
        }

    }
}
