namespace SelfZNab.SharedInTests;

public class ExpectedXml
{
    public static string GetXml() =>
        @"<caps xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><server title=""SelZNab"" version=""1.0"" url=""http://localhost:5044/api/torrents"" /><limits max=""100"" default=""50"" /><registration available=""no"" open=""no"" /><features><feature name=""search"" available=""yes"" /><feature name=""tv-search"" available=""yes"" /><feature name=""movie-search"" available=""yes"" /><feature name=""anime-search"" available=""yes"" /></features><searching><search available=""yes"" supportedParams=""q"" /><tv-search available=""yes"" supportedParams=""q,season,ep,imdbid,tmdbid,tvdbid"" /><movie-search available=""yes"" supportedParams=""q,imdbid,tmdbid"" /><anime-search available=""yes"" supportedParams=""q,season,ep,anidb"" /></searching><categories><category id=""2000"" name=""Movies""><subcat id=""2010"" name=""HD"" /><subcat id=""2020"" name=""UHD"" /><subcat id=""2030"" name=""SD"" /><subcat id=""2040"" name=""BluRay"" /></category><category id=""5000"" name=""TV""><subcat id=""5030"" name=""HD"" /><subcat id=""5040"" name=""UHD"" /><subcat id=""5050"" name=""SD"" /><subcat id=""5060"" name=""BluRay"" /></category><category id=""5070"" name=""Anime"" /></categories></caps>";
}
