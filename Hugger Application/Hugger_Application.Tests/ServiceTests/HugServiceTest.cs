using AutoMapper;
using Hugger_Application.Data.Repository.HugRepository;
using Hugger_Application.Services.HugService;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugger_Application.Tests.ServiceTests
{
 
    [TestFixture]
    public class HugServiceTest
    {
        [SetUp]
        public void Setup()
        {
            var mockedRepository = new Mock<IHugRepository>().Object;
            var mockedMapper = new Mock<IMapper>().Object;
            var mockedLogger = new Mock<ILogger<HugService>>().Object;

            var hugService = new HugService(mockedRepository,mockedMapper, mockedLogger);
        }
                 


    }
}
