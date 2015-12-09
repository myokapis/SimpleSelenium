using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Xml;
using System.Threading.Tasks;
using NUnit.Framework;
using CAMSComponents.Components;
using CAMSDataAccess.Contexts;
using CAMSDataAccess.Models;
using Moq;

namespace CAMSComponents.Tests
{
  [TestFixture]
  public class TestQuestionnaireComponent
  {
    private IQueryable<Questionnaire> _questionnaires;
    private Mock<DbSet<Questionnaire>> _mockDbSet;
    private Mock<QuestionnaireContext> _mockDbContext;
    private QuestionnaireContext _questionnaireContext;

    #region "setup & teardown"

    [SetUp]
    public void SetupTest()
    {
      List<Questionnaire> questionnaires = new List<Questionnaire>();

      foreach (Dictionary<string, object> dic in QuestionnaireData())
      {
        questionnaires.Add(CreateQuestionnaire(dic));
      }

      _questionnaires = questionnaires.AsQueryable<Questionnaire>();
      MockQuestionnaireContext();
      _questionnaireContext = _mockDbContext.Object;
    }

    [TearDown]
    public void TeardownTest()
    {
      // TODO: dispose of anything that supports Dispose
    }

    #endregion

    #region "tests"

    // tests the case where a new questionnaire of the latest version is created and returned
    [Test]
    public void TestGetQuestionnaireXmlDocument_CreateLatestVersion()
    {
      // setup data
      Dictionary<string, object> data = new Dictionary<string, object>(QuestionnaireData()[0]);
      int OwnerId = 999;
      string QuestionnaireType = data["TypeCode"].ToString();
      int InstanceNo = (int)data["InstanceNo"];
      string User = "TestUser";

      // setup objects to test
      QuestionnaireComponent component = new QuestionnaireComponent(_questionnaireContext);
      XmlDocument document = component.GetQuestionnaireXmlDocument(OwnerId, QuestionnaireType, InstanceNo, User);

      Assert.AreEqual(OwnerId, int.Parse(document.FirstChild.Attributes["owner-id"].Value));
      Assert.AreEqual(QuestionnaireType, document.FirstChild.Attributes["type"].Value);
      Assert.AreEqual(InstanceNo, int.Parse(document.FirstChild.Attributes["instance"].Value));
      _mockDbSet.Verify(m => m.Add(It.IsAny<Questionnaire>()), Times.Once);
      _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    // tests the case where a new questionnaire of a specific version is created and returned
    [Test]
    public void TestGetQuestionnaireXmlDocument_CreateSpecificVersion()
    {
      // setup data
      Dictionary<string, object> data = new Dictionary<string, object>(QuestionnaireData()[0]);
      int OwnerId = 999;
      string QuestionnaireType = data["TypeCode"].ToString();
      int InstanceNo = (int)data["InstanceNo"];
      string User = "TestUser";
      string Version = data["VersionNo"].ToString();

      // setup objects to test
      QuestionnaireComponent component = new QuestionnaireComponent(_questionnaireContext);
      XmlDocument document = component.GetQuestionnaireXmlDocument(OwnerId, QuestionnaireType, InstanceNo, User, Version);

      Assert.AreEqual(OwnerId, int.Parse(document.FirstChild.Attributes["owner-id"].Value));
      Assert.AreEqual(QuestionnaireType, document.FirstChild.Attributes["type"].Value);
      Assert.AreEqual(InstanceNo, int.Parse(document.FirstChild.Attributes["instance"].Value));
      Assert.AreEqual(Version, (document.FirstChild.Attributes["version"].Value.ToString()));
      _mockDbSet.Verify(m => m.Add(It.IsAny<Questionnaire>()), Times.Once);
      _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    // tests the case where an existing questionnaire is returned
    [Test]
    public void TestGetQuestionnaireXmlDocument_Find()
    {
      // setup data
      Dictionary<string, object> data = new Dictionary<string, object>(QuestionnaireData()[0]);
      int OwnerId = (int)data["OwnerId"];
      string QuestionnaireType = data["TypeCode"].ToString();
      int InstanceNo = (int)data["InstanceNo"];
      string User = "TestUser";

      // setup objects to test
      QuestionnaireComponent component = new QuestionnaireComponent(_questionnaireContext);
      XmlDocument document = component.GetQuestionnaireXmlDocument(OwnerId, QuestionnaireType, InstanceNo, User);

      Assert.AreEqual(data["Id"], int.Parse(document.FirstChild.Attributes["id"].Value));
      _mockDbSet.Verify(m => m.Add(It.IsAny<Questionnaire>()), Times.Never);
      _mockDbContext.Verify(m => m.SaveChanges(), Times.Never);
    }

    // tests the case where the questionnaire is not found and can't be created
    [Test]
    public void TestGetQuestionnaireXmlDocument_NoRecordFound()
    {
      // setup data
      int OwnerId = 999;
      string QuestionnaireType = "Bogus";
      int InstanceNo = 3;
      string User = "TestUser";

      // setup objects to test
      QuestionnaireComponent component = new QuestionnaireComponent(_questionnaireContext);

      Exception ex = Assert.Throws<NullReferenceException>(() => component.GetQuestionnaireXmlDocument(OwnerId, QuestionnaireType, InstanceNo, User));
      _mockDbSet.Verify(m => m.Add(It.IsAny<Questionnaire>()), Times.Never);
      _mockDbContext.Verify(m => m.SaveChanges(), Times.Never);
    }

    // tests the case where the xml is invalid and can't be parsed
    [Test]
    public void TestSaveQuestionnaireXmlDocument_InvalidXml()
    {
      // setup data
      string user = "TestUser";
      string xmlData = "<questionnaire></questionnaire>";

      // setup objects to test
      QuestionnaireComponent component = new QuestionnaireComponent(_questionnaireContext);

      Assert.Throws<NullReferenceException>(() => component.SaveQuestionnaireXmlDocument(xmlData, user));
      _mockDbContext.Verify(m => m.SaveChanges(), Times.Never);
    }

    // tests the case where the requested questionnaire can't be found
    [Test]
    public void TestSaveQuestionnaireXmlDocument_NotFound()
    {
      // setup data
      string user = "TestUser";
      string xmlData = "<questionnaire owner-id=\"12345\" type=\"UnknownType\" instance=\"0\"></questionnaire>";

      // setup objects to test
      QuestionnaireComponent component = new QuestionnaireComponent(_questionnaireContext);

      Assert.Throws<NullReferenceException>(() => component.SaveQuestionnaireXmlDocument(xmlData, user));
      _mockDbContext.Verify(m => m.SaveChanges(), Times.Never);
    }

    // tests the case where the questionnaire is successfully updated
    [Test]
    public void TestSaveQuestionnaireXmlDocument_Updated()
    {
      // setup data
      Dictionary<string, object> data = new Dictionary<string, object>(QuestionnaireData()[0]);
      Questionnaire questionnaire = CreateQuestionnaire(data);
      //int OwnerId = (int)data["OwnerId"];
      //string QuestionnaireType = data["TypeCode"].ToString();
      //int InstanceNo = (int)data["InstanceNo"];
      string User = "TestUser";

      // setup objects to test
      QuestionnaireComponent component = new QuestionnaireComponent(_questionnaireContext);

      component.SaveQuestionnaireXmlDocument(questionnaire.ToXml().InnerXml, User);
      _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    #endregion

    #region "Helper Methods"

    // TODO: refactor these helper methods into a common, reusable object

    private List<Dictionary<string, object>> QuestionnaireData()
    {
      List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>()
      {
        new Dictionary<string, object>()
        {
          {"Id", 1},
          {"OwnerId", 1234},
          {"TypeCode", "Questionnaire1"},
          {"InstanceNo", 144},
          {"Name", "Test Questionnaire"},
          {"Description", "Test Questionnaire Desc"},
          {"VersionNo", "1.1.0"},
          {"IsComplete", false},
          {"IsTemplate", false},
          {"InsertUser", "INSUSR"},
          {"InsertTime", new DateTime()},
          {"UpdateUser", "UPDUSR"},
          {"UpdateTime", new DateTime().AddMinutes(387.00)},
          {"XmlContents", "<sections><section>This section</section></sections>"}
        },
        new Dictionary<string, object>()
        {
          {"Id", 2},
          {"OwnerId", 0},
          {"TypeCode", "Questionnaire1"},
          {"InstanceNo", 145},
          {"Name", "Test Questionnaire"},
          {"Description", "Test Questionnaire Desc"},
          {"VersionNo", "1.1.0"},
          {"IsComplete", false},
          {"IsTemplate", true},
          {"InsertUser", "SYSTEM"},
          {"InsertTime", new DateTime()},
          {"UpdateUser", "USER1"},
          {"UpdateTime", new DateTime().AddMinutes(333.00)},
          {"XmlContents", "<sections><section>Template 1</section></sections>"}
        },
        new Dictionary<string, object>()
        {
          {"Id", 3},
          {"OwnerId", 12356},
          {"TypeCode", "Questionnaire2"},
          {"InstanceNo", 14},
          {"Name", "Test Questionnaire 2"},
          {"Description", "Test Questionnaire 2 Desc"},
          {"VersionNo", "1.1.0"},
          {"IsComplete", true},
          {"IsTemplate", false},
          {"InsertUser", "INSUSR"},
          {"InsertTime", new DateTime()},
          {"UpdateUser", "UPDUSR"},
          {"UpdateTime", new DateTime().AddMinutes(444.00)},
          {"XmlContents", "<sections><section>Test Questionnaire 2 Section 1</section></sections>"}
        }
      };

      return dataList;
    }

    private Questionnaire CreateQuestionnaire(Dictionary<string, object> Values)
    {
      Questionnaire q = new Questionnaire()
      {
        Id = (int)Values["Id"],
        OwnerId = (int)Values["OwnerId"],
        TypeCode = Values["TypeCode"].ToString(),
        InstanceNo = (int)Values["InstanceNo"],
        Name = Values["Name"].ToString(),
        Description = Values["Description"].ToString(),
        VersionNo = Values["VersionNo"].ToString(),
        IsComplete = (bool)Values["IsComplete"],
        IsTemplate = (bool)Values["IsTemplate"],
        InsertUser = Values["InsertUser"].ToString(),
        InsertTime = (DateTime)Values["InsertTime"],
        UpdateUser = Values["UpdateUser"].ToString(),
        UpdateTime = (DateTime)Values["UpdateTime"],
        XmlContents = Values["XmlContents"].ToString()
      };

      return q;
    }

    private void MockQuestionnaireContext()
    {
      // setup mock DbSet class
      _mockDbSet = new Mock<DbSet<Questionnaire>>();
      _mockDbSet.As<IQueryable<Questionnaire>>().Setup(m => m.Provider).Returns(_questionnaires.Provider);
      _mockDbSet.As<IQueryable<Questionnaire>>().Setup(m => m.Expression).Returns(_questionnaires.Expression);
      _mockDbSet.As<IQueryable<Questionnaire>>().Setup(m => m.ElementType).Returns(_questionnaires.ElementType);
      _mockDbSet.As<IQueryable<Questionnaire>>().Setup(m => m.GetEnumerator()).Returns(_questionnaires.GetEnumerator());

      // setup mock DbContext class
      _mockDbContext = new Mock<QuestionnaireContext>();
      _mockDbContext.Setup(c => c.Questionnaires).Returns(_mockDbSet.Object);
    }

    #endregion
  }
}
