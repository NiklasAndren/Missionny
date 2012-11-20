using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mission.Domain.Entities.Abstract;
using Mission.Domain.Entities;



namespace Mission.Domain.Entities.FakeData 
{
    public class FakeData 
    {
    //Fake users

        public Post firstTestPost;
        public Post secondTestPost;
        public Post thirdTestPost;

        public Post firstTestNews;
        public Post secondTestNews;
        public Post thirdTestNews;

        public Event firstTestEvent;
        public Event secondTestEvent;
        public Event thirdTestEvent;

        public Comment firstTestComment;
        public Comment secondTestComment;

        public Answer firstTestAnswer;
        public Answer firstTestAnswer2;
        public Answer secondTestAnswer;
        public Answer secondTestAnswer2;

        //public Words firstWord;
        //public Words secondWord;
        //public Words thirdWord;

        public EventQuestion firstTestEventQuestion;
        public EventQuestion secondTestEventQuestion;

        public FakeData()
        {

            firstTestPost = new Post { ID = Guid.NewGuid(), Body = "<img data-oid=\"508e9a21ddf2b3313e000272\" class=\"image\" alt=\"\" src=\"http://cdn.publishme.se/cdn/9-1/1868412/images/2012/img_3726_508e9a21ddf2b3313e000272.jpg\" style=\"border: 0px none;\"><div>Hemkommen igen efter ett par intensiva dagar i Stockholm. Efter att ha segrat på lokal nivå och på regional nivå så var det dags för Skandinaviska mästerskapen i grenen humortal. Har fått en hel del frågor på vad den här tävlingen går ut på så här kommer lite små fakta.</div><div> </div><div>Tävlingen arrangeras av Toastmasters International och är världens största humortävling. För att få lite proportioner på det hela kan sägas att det krävs hela 150 000 funktionärer för att rodda hela tävlingen runt om i världen. Under London OS hade man 70 000 funktionärer.</div><div> </div><div>Som tävlande skall du sätta samman ett tal som skall vara mellan 5-7 minuter. Om du vid tävlingstillfället skulle avsluta före 4:30 och efter 7:30 så är du diskad. Utifrån en betygsmall som innefattar ett antal olika bedömningspunkter så som talupplägg, rekvisita, publik reaktioner, oväntade kast och överraskningsmoment, förmågan att uppnå syftet med talet, idéer och orginalitet, kroppsspråk, röstdynamik, säkerhet, entusiasm, lämplighet, god smak, uttal, ordval etc.</div><div> </div><div>Alla dess bedömningspunkter är jätteviktiga. Jag har sett väldigt prominenta komiker åka ut tidigt just på grund av att de slarvat med detta. Talet var för all del jätteroligt men det fanns inget mer.</div><div> </div><div>För egen del har jag nu vunnit lokala mästerskapet i Borås, regionala mästerskapet i Göteborg och nu Skandinavienmästerskapet i Stockholm.</div><div> </div><div>16-18 november åker jag till Bonn i Tyskland för att tävla i EM.</div><div> </div><div>Jag har egentligen aldrig betraktat mig som rolig även om jag alltid gillat att skoja. I många år var det stora problemet just att jag var den enda som skrattade åt mina skämt. Under mina sex år som föreläsare har jag dock jobbat hårt med det här. Som föreläsare är det viktigt att kunna locka fram skratt. Det lättar upp att få skratta av sig lite grann när man talar om de svårare ämnena. Skratt har en sådan förlösande effekt.</div><div> </div><div>Principen jag har använt för att plocka fram mitt material är en enorm envishet. Jag har öppnat många musslor för att mina skrattpärlor med andra ord. Nu återstår ett par veckor där det gäller att laborera och trixa med talet för att plocka upp det ett antal pinnhål till.</div><div> </div><div>Jag skall göra allt jag kan för att bli Europamästare!</div>", Date = DateTime.Parse("2011-07-24"), Title = "Jesper Caron Skandinavisk mästare i humortal", Type = (int)Type.Blog };
            secondTestPost = new Post { ID = Guid.NewGuid(), Body = "<img data-oid=\"50813cfc9606ee030ea9a71d\" class=\"image\" alt=\"\" src=\"http://cdn.publishme.se/cdn/9-1/1868412/images/2012/img_3651_50813cfc9606ee030ea9a71d.jpg\" style=\"border: 0px none;\"><div>Slog nytt rekord när jag var och tankade idag. Förutom nivån på skatter och avgifter är det som bekant utbud och efterfrågan som styr prisutvecklingen. På senare år har vi inte haft några direkta skattehöjningar så då måste det vara marknadskrafterna som verkar. Träffade en nationalekonom som sade att oljepriset ofta går ner under lågkonjunktur eftersom efterfrågan minskar. Inte denna lågkonjunkturen dock, den värsta sedan 30-talet.</div><div> </div><div>Min giss är att vi nu upplever det som kallas för PeakOil. Runt om i världen har flera historiskt stora fyndigheter redan sinat eller håller på att sina. Man får leta sig till mer och mer ogästvänliga och svårtillgängliga platser för att bryta oljan. Tyvärr har oljan räckt för länge. Det hade varit bra om den tagit slut redan under 80-talet med tanke på den globala uppvärmningen.</div><div> </div><div>Det är spännande att fundar kring vad som hade hänt med oljepriset om vi hade gått in i en gigantisk högkonjunktur just nu? Om efterfrågan hade ökat radikalt? Helt klart hade vi då fått se bränsleräkningar som får min rekordtankning att verka som rena fyndet.</div>", Date = DateTime.Parse("2012-05-05"), Title = "PeakOil eller?", Type = (int)Type.Blog };
            thirdTestPost = new Post { ID = Guid.NewGuid(), Body = "test body 3", Date = DateTime.Parse("2011-03-17"), Title = "test title 3", Type = (int)Type.Blog};

            firstTestNews = new Post { ID = Guid.NewGuid(), Body = "Hej VännerDå var det dags för ett nummer av In Action igen.  Håller tummarna att Du kommer att bli nöjd. Stort tack för förtroendet jag får av er. Dagens nummer gäller något som är ultraviktigt för oss alla att göra med jämna mellanrum. Du finner läsningen under länken nedan.http://jespercaron.blogg.se/2012/october/renovera-mera.htmlTorkild Sköld och Jesper Caron föreläser tillsammans!En person som jag har fått förmånen att lära känna de senaste åren är Torkild Sköld. En makalös människa med stort hjärta och en gedigen bakgrund. Att branschen sedan har utsett honom till Årets bästa nya föreläsare 2011 är hur rättvist som helst. Han är nämligen helt otrolig på scenen. Därför känns det särskilt kul att berätta att vi kör ett par föreläsningar tillsammans i höst. Se till att hugga möjligheten vet ja!Vi kommer till Borås, Malmö och Göteborg. Om du är intresserad så Boka dig här!Borås 24/10 http://simplesignup.se/event/13676Malmö 30/10 http://simplesignup.se/event/13697Göteborg 6/11 http://simplesignup.se/event/13715Lev Nu - För allt vad du är värd!/Jesper CaronPrenumeration på In ActionOm du tycker att In Action är av värde för dig och Materialet gör skillnad i ditt liv så är jag tacksam om du rekommenderar det vidare till andra. Att bli prenumerant är mycket enkelt. Man besöker www.jespercaron.se och klickar på knappen ”IN Action”. Därefter fyller man i formuläret och sedan är man prenumerant!Copyright © Actionplant AB - Innehållet i IN ACTION får kopieras och distribueras för ickekommersiellt bruk så länge som författarens namn, hemsida och kontaktinformation medföljer. Copyrighten upprätthålls av din personliga ärlighet och integritet.", Date = DateTime.Parse("2011-04-24"), Title = "Renovera mera", Type = (int)Type.News };
            secondTestNews = new Post { ID = Guid.NewGuid(), Body = "test body 2 neweews", Date = DateTime.Now, Title = "test title 2", Type = (int)Type.News};
            thirdTestNews = new Post { ID = Guid.NewGuid(), Body = "test body 3   news?", Date = DateTime.Now, Title = "test title 3", Type = (int)Type.News};

            firstTestEvent = new Event {  ID = Guid.NewGuid(), City = "Göteborg", Date = DateTime.Now, HeadLine = "Test headline 1", Description = "oiqewoihewfoiewofiewohfoiwhf" };
            secondTestEvent = new Event { ID = Guid.NewGuid(), City = "Stockholm", Date = DateTime.Now, HeadLine = "Test headline 2", Description = "oi576767867867687678768f" };
            thirdTestEvent = new Event { ID = Guid.NewGuid(), City = "Bankeryd", Date = DateTime.Parse("2011-11-11"), HeadLine = "Upp med motivationen", Description = "Nu är det dag att bli av med denna trista vinterdepressionen ni alla har börjat bygga upp", Company="vbs", OpenEvent =(int)OpenEvent.Closed,   };

            firstTestComment = new Comment { ID = Guid.NewGuid(), Body = "Svindyrt! Giriga as är vad dom är.", Name = "Alan", PostID = secondTestPost.ID, Date = DateTime.Parse("2010-05-06") };
            secondTestComment = new Comment { ID = Guid.NewGuid(), Body = "Det har du helt rätt i men det är som det är.", Name = "Bert", PostID = secondTestPost.ID, Date = DateTime.Parse("2010-05-07") };

            firstTestEventQuestion = new EventQuestion { ID = Guid.NewGuid(), Date = DateTime.Parse("2011-11-11"), EventID = thirdTestEvent.ID ,Question = "1. Pedagogik - Vad tycker du om Jesper Carons förmåga att lära ut?" };
            secondTestEventQuestion = new EventQuestion { ID = Guid.NewGuid(), Date = DateTime.Parse("2011-11-11"), EventID = thirdTestEvent.ID, Question = "2. Föreläsningen - Vad tycker du om innehållet i Jesper Carons föreläsning?" };


           // firstTestAnswer = new Answer { ID = Guid.NewGuid(), Gender = (int)Gender.Male, Age = 54, Score = 5, EventQuestionID = firstTestEventQuestion.ID, Username = "Gunnar" };
            //firstTestAnswer2 = new Answer { ID = Guid.NewGuid(), Gender = (int)Gender.Male, Age = 54, Score = 5, EventQuestionID = secondTestEventQuestion.ID, Username = "Gunnar" };
            //secondTestAnswer = new Answer { ID = Guid.NewGuid(), Gender = (int)Gender.Female, Age = 43, Score = 4, EventQuestionID = firstTestEventQuestion.ID, Username = "Lisa" };
            //secondTestAnswer2 = new Answer { ID = Guid.NewGuid(), Gender = (int)Gender.Female, Age = 43, Score = 3, EventQuestionID = secondTestEventQuestion.ID, Username = "Lisa" };
       
        }
        public List<Post> testPostList() { return new List<Post> { firstTestPost, secondTestPost, thirdTestPost }; } 
        public List<Post> testNewsList() { return new List<Post> { firstTestNews, secondTestNews, thirdTestNews };  }
        public List<Event> testEventList() { return new List<Event> { firstTestEvent, secondTestEvent, thirdTestEvent }; }
        public List<Comment> testCommentList() { return new List<Comment> { firstTestComment, secondTestComment }; }
        public List<EventQuestion> testEventQuestionList() { return new List<EventQuestion> { firstTestEventQuestion, secondTestEventQuestion }; }
        //public List<Answer> testAnswerList() { return new List<Answer> { firstTestAnswer, firstTestAnswer2, secondTestAnswer, secondTestAnswer2 }; }
        
    }
}
