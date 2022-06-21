using AtHome.APIHandler.interfaces;
using AtHome.ShippingCompanyAPI.interfaces;
using AtHome.ShippingCompanyAPI.handlers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using AtHome.Domain;
using AtHome.ShippingCompanyAPI.resolvers;
using System.Collections.Generic;

namespace AtHome.ShippingCompanyAPI.Test;

public class SCHandlerResolverTest
{
    private ISCHandlerResolver _scHandlerResolver;

    [SetUp]
    public void Setup()
    {

        IList<ISCApiHandler> scHandlers = new List<ISCApiHandler>();
        scHandlers.Add(new RestApiHandler());
        scHandlers.Add(new SoapApiHandler());
        scHandlers.Add(new RpcApiHandler());

        _scHandlerResolver = new SCHandlerResolver(scHandlers);



    }

    [Test]
    public void TestGetApiHandler()
    {
        ISCApiHandler handler = _scHandlerResolver.Resolve(ServiceType.REST);

        Assert.AreEqual(typeof(RestApiHandler).ToString(), handler.GetType().ToString());

        handler = _scHandlerResolver.Resolve(ServiceType.SOAP);

        Assert.AreEqual(typeof(SoapApiHandler).ToString(), handler.GetType().ToString());

        handler = _scHandlerResolver.Resolve(ServiceType.RPC);

        Assert.AreEqual(typeof(RpcApiHandler).ToString(), handler.GetType().ToString());

        Assert.Pass();
    }
}