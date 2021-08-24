﻿using NUnit.Framework;
using System.Collections.Generic;

namespace webAddressbookTests
{
    [TestFixture]
    public class DeleteContactTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();
            ContactData toBeRemoved = oldContacts[0];

            app.Contacts.RemoveContact(1, true);
            app.Navigator.ReturnToHomePage();

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
