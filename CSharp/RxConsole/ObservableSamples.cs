﻿using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace CSharpRxConsole
{
    public class BenchmarSamples
    {
        Service _service;

        public BenchmarSamples()
        {
            _service = new Service(new HttpClient());
        }

        [Benchmark]
        public async Task GetUsers_Observable()
        {
            var users = await _service.GetObservableUsers();

            users
                .Where(u => u.LastName.StartsWith("F"))
                .Select(u => new User() { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email.Replace("@reqres.in", "@rxjs.dev"), Avatar = u.Avatar })
                .Subscribe(u =>
                {
                    Console.WriteLine($"   - {u.FirstName} {u.LastName} - {u.Email}");
                });
        }


        [Benchmark]
        public async Task GetUsers_ToObservable()
        {
            var users = await _service.GetUsersToObservable();

            users
                .Where(u => u.LastName.StartsWith("F"))
                .Select(u => new User() { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email.Replace("@reqres.in", "@rxjs.dev"), Avatar = u.Avatar })

                // OPTION 1)
                //.Subscribe(
                //    onNext: u => Console.WriteLine($"   - {u.FirstName} {u.LastName} - {u.Email}"),
                //    onError: err => { },
                //    onCompleted: () => { }
                // );

                // OPTION 2)
                .Subscribe(u => Console.WriteLine($"   - {u.FirstName} {u.LastName} - {u.Email}"));
        }
    }
}
