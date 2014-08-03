namespace Audition.Data.Service.UnitTests
{
    public static class TestHelper
    {
        public static string MakeBadJsonString()
        {
            return "This is bad!";
        }

        public static string MakeTestProductDataJson()
        {
            const string json =
                "[" +
                "{'brand':'A','formatted_regular_price':'$1.00','image_url':'ia','name':'na','style_id':'1'}, " +
                "{'brand':'A','formatted_regular_price':'$1.00','image_url':'ia','name':'na','style_id':'1'}, " + // dup styleId
                "{'brand':'B','formatted_regular_price':'$2.00','image_url':'ib','name':'nb','style_id':'2'}, " +
                "{'brand':'C','formatted_regular_price':'$3.00','image_url':'ic','name':'nc','style_id':'3'}, " +
                "]";

            return json;
        }

        public static string JsonProducerThatThrowsException()
        {
            throw new SomeWeirdException();
        }
    }
}