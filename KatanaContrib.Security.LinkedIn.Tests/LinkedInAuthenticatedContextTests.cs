﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KatanaContrib.Security.LinkedIn.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Owin;
using Newtonsoft.Json.Linq;
using Moq;

namespace KatanaContrib.Security.LinkedIn.Tests
{
    [TestClass]
    public class LinkedInAuthenticatedContextTests
    {
        /// <summary>
        /// A test to verify when parameter named 'user' is null, an exception of type ArgumentNullException should be thrown.
        /// </summary>
        [TestMethod]
        public void LinkedInAuthenticatedContext_WhenUserParameterIsNull_ShouldThrowArgumentNullException()
        {
            IOwinContext context = CreateStubOwinContext();
            JObject user = null;
            string accessToken = "2975638759247325yugfysh8274585";
            string expires = "3600";

            try
            {
                LinkedInAuthenticatedContext linkedinContext = new LinkedInAuthenticatedContext(context, user, accessToken, expires);
            }
            catch(ArgumentNullException e)
            {
                StringAssert.Contains(e.Message, "user is null");
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        /// <summary>
        ///  A test to verify when parameter named 'accessToken' is null, an exception of type ArgumentNullException should be thrown.
        /// </summary>
        [TestMethod]
        public void LinkedInAuthenticatedContext_WhenAccessTokenParameterIsNull_ShouldThrowArgumentNullException()
        {
            IOwinContext context = CreateStubOwinContext();
            string userInfo = "{\"id\":\"3lwM3bUvfJ\",\"first-name\":\"Nirosha\",\"last-name\":\"Gihan\",\"formatted-name\":\"Nirosha Gihan\"}";
            JObject user = JObject.Parse(userInfo);
            string accessToken = null;
            string expires = "3600";

            try
            {
                LinkedInAuthenticatedContext linkedinContext = new LinkedInAuthenticatedContext(context, user, accessToken, expires);
            }
            catch (ArgumentNullException e)
            {
                StringAssert.Contains(e.Message, "access token is null");
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        /// <summary>
        /// A test to verify when parameter named 'context' is null, an exception of type ArgumentNullException should be thrown.
        /// </summary>
        [TestMethod]
        public void LinkedInAuthenticatedContext_WhenContextParameterIsNull_ShouldThrowArgumentNullException()
        {
            IOwinContext context = null;
            string userInfo = "{\"id\":\"3lwM3bUvfJ\",\"first-name\":\"Nirosha\",\"last-name\":\"Gihan\",\"formatted-name\":\"Nirosha Gihan\"}";
            JObject user = JObject.Parse(userInfo);
            string accessToken = "2975638759247325yugfysh8274585";
            string expires = "3600";

            try
            {
                LinkedInAuthenticatedContext linkedinContext = new LinkedInAuthenticatedContext(context, user, accessToken, expires);
            }
            catch (ArgumentNullException e)
            {
                StringAssert.Contains(e.Message, "context is null");
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        /// <summary>
        /// A test to verify when the parameter named 'Expires' is null, an exception of type ArgumentNullException should be thrown.
        /// </summary>
        [TestMethod]
        public void LinkedInAuthenticatedContext_WhenExpiresParameterIsNull_ShouldThrowArgumentNullException()
        {
            IOwinContext context = CreateStubOwinContext();
            string userInfo = "{\"id\":\"3lwM3bUvfJ\",\"first-name\":\"Nirosha\",\"last-name\":\"Gihan\",\"formatted-name\":\"Nirosha Gihan\"}";
            JObject user = JObject.Parse(userInfo);
            string accessToken = "7654hjgsgf384hjgfvfdsk3847bhfjvh3485634";
            string expires = null;

            try
            {
                LinkedInAuthenticatedContext linkedinContext = new LinkedInAuthenticatedContext(context, user, accessToken, expires);
            }
            catch (ArgumentNullException e)
            {
                StringAssert.Contains(e.Message, "expires parameter is null");
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        /// <summary>
        /// A test to verify when the parameter 'expires' is not a number, an exception of type ArgumentOutofRange exception should be thrown.
        /// </summary>
        [TestMethod]
        public void LinkedInAuthenticatedContext_WhenExpiresParameterIsNotaNumber_ShouldThrowArgumentOutOfRangeException()
        {
            IOwinContext context = CreateStubOwinContext();
            string userInfo = "{\"id\":\"3lwM3bUvfJ\",\"first-name\":\"Nirosha\",\"last-name\":\"Gihan\",\"formatted-name\":\"Nirosha Gihan\"}";
            JObject user = JObject.Parse(userInfo);
            string accessToken = "7654hjgsgf384hjgfvfdsk3847bhfjvh3485634";
            string expires = "hello world";

            try
            {
                LinkedInAuthenticatedContext linkedInContext = new LinkedInAuthenticatedContext(context, user, accessToken, expires);
            }
            catch(ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, "expires value should be a number");
                return;
            }

            Assert.Fail("No exception was thrown");
        }

        private static IOwinContext CreateStubOwinContext()
        {
            return CreateOwinContextMock().Object;
        }

        private static Mock<IOwinContext> CreateOwinContextMock()
        {
            Mock<IOwinContext> mock = new Mock<IOwinContext>(MockBehavior.Strict);
            mock.Setup(c => c.Request).Returns(CreateDummyOwinRequest());
            return mock;
        }

        private static IOwinRequest CreateDummyOwinRequest()
        {
            return new Mock<IOwinRequest>(MockBehavior.Strict).Object;
        }
    }
}
