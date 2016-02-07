﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using System;

namespace NLogToSlack.Tests
{
    [TestClass]
    public class SlackTargetTests
    {
        [TestMethod]
        public void DefaultSettings_ShouldBeCorrect()
        {
            var slackTarget = new TestableSlackTarget();

            slackTarget.Channel.Should().Be(null);
            slackTarget.Icon.Should().Be(null);
            slackTarget.Username.Should().Be(null);
            slackTarget.WebHookUrl.Should().Be(null);
            slackTarget.ExcludeLevel.Should().BeFalse();
        }

        [TestMethod]
        public void CustomSettings_ShouldBeCorrect()
        {
            const string channel = "#log-${level}";
            const string icon = ":ghost:";
            const string username = "NLogToSlack-${level}";
            const string webHookUrl = "http://slack.is.awesome.com";
            const bool excludeLevel = true;

            var slackTarget = new TestableSlackTarget
            {
                Channel = channel,
                Icon = icon,
                Username = username,
                WebHookUrl = webHookUrl,
                ExcludeLevel = excludeLevel
            };

            var logEvent = new LogEventInfo { Level = LogLevel.Info, Message = "This is a ${level} message" };

            slackTarget.Channel.Render(logEvent).Should().Be("#log-Info");
            slackTarget.Username.Render(logEvent).Should().Be("NLogToSlack-Info");
            slackTarget.Icon.Should().Be(icon);
            slackTarget.WebHookUrl.Should().Be(webHookUrl);
            slackTarget.ExcludeLevel.Should().BeTrue();


        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InitializeTarget_EmptyWebHookUrl_ShouldThrowException()
        {
            var slackTarget = new TestableSlackTarget();

            slackTarget.Initialize();
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InitializeTarget_IncorrectWebHookUrl_ShouldThrowException()
        {
            var slackTarget = new TestableSlackTarget
            {
                WebHookUrl = "IM A BIG FAT PHONY"
            };

            slackTarget.Initialize();
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InitializeTarget_IncorrectChannel_ShouldThrowException()
        {
            var slackTarget = new TestableSlackTarget
            {
                WebHookUrl = "http://slack.is.awesome.com",
                Channel = "wrong"
            };

            slackTarget.Initialize();
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InitializeTarget_IncorrectChannel_ExtraCharWithAt_ShouldThrowException()
        {
            var slackTarget = new TestableSlackTarget
            {
                WebHookUrl = "http://slack.is.awesome.com",
                Channel = "w@slackbot"
            };

            slackTarget.Initialize();
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InitializeTarget_IncorrectChannel_ExtraCharWithHash_ShouldThrowException()
        {
            var slackTarget = new TestableSlackTarget
            {
                WebHookUrl = "http://slack.is.awesome.com",
                Channel = "w#log"
            };

            slackTarget.Initialize();
        }
        [TestMethod]
        public void InitializeTarget_CorrectChannelWithHash_TargetShouldInitialize()
        {
            var slackTarget = new TestableSlackTarget
            {
                WebHookUrl = "http://slack.is.awesome.com",
                Channel = "#log"
            };

            slackTarget.Initialize();
        }
        [TestMethod]
        public void InitializeTarget_CorrectChannelWithAt_TargetShouldInitialize()
        {
            var slackTarget = new TestableSlackTarget
            {
                WebHookUrl = "http://slack.is.awesome.com",
                Channel = "@slackbot"
            };

            slackTarget.Initialize();
        }
        [TestMethod]
        public void InitializeTarget_CorrectChannelWithVariable_TargetShouldInitialize()
        {
            var slackTarget = new TestableSlackTarget
            {
                WebHookUrl = "http://slack.is.awesome.com",
                Channel = "@slackbot"
            };

            slackTarget.Initialize();
        }
    }
}