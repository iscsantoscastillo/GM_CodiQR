﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApiCoDi.GeneracionCodigo.Peticion;
using WebApiCoDi.GeneracionCodigo.Respuesta;
using WebApiCoDi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCoDi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CodiController : ControllerBase
    {
        // GET: api/<CodiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Api Codi" };
        }

        // GET api/<CodiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CodiController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CuerpoRespuesta>> Post([FromBody] CuerpoPeticion objPeticion)
        {
            try
            {
                objPeticion.ValidarPropiedades();
                return Ok(QR.GenerarQRDatosBase(objPeticion));

                //return Ok(QR.GenerarQR(objPeticion));

                //if (objPeticion.referencia == "SL202100007822")
                //{
                //    CuerpoRespuesta respuesta = new CuerpoRespuesta();
                //    respuesta.hayError = false;
                //    respuesta.mensajeError = "";
                //    respuesta.qrBase64 = "iVBORw0KGgoAAAANSUhEUgAAAZcAAAGZCAMAAAEFtbzZAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAnUExURf///9TU1MjIyPPz82JiYjc3NwAAAAwMDCsrK52dnVZWVqmpqQAAAMD36O0AAAANdFJOU////////////////wA96CKGAAAACXBIWXMAAA7DAAAOwwHHb6hkAAAr0ElEQVR4Xu2dgXbcOK5Ed3Yne7Iz//+/DyCKEouCqiFaamfy+h7HEkEABVIKTcuK868Vfvx3wFr/+hcOfrIfWjew7n93WuuHJWkxdvK/f3vMvyMRnLwVKRvNGYla5hbipbQ+gJhw7PZ2aKaIaYeWyLknxu3m4cZ22FroawcYvLEag3OnRTVzWlubQ8S0c78AP3601k87j5g/WlePAS2mEeLt1FIjBrSYge6cHXaWYpjNC32Jy4FaDIT/3bpxMHM7oO5tDvxT2H/8NPN/2iRbd5vPv374wUL88N+fP2zC7bMZ4y7fynBaSsPFI2UcgLeir51TaMOaWwbv6Z+a8Usx3jCa3T5FN8ytDc9uvRTjYdZA99Bq3d0rQJTdrb5q2Fz7TWzdPvMW8+Nn62tT/Lel+Bl3ud/uftajHc9seG2tjVarIbA2hGFv2FmU2Ezh5Z8wyMWYzd7Ond0YU9H6rD1kX4ppxOBR1GA3oscPrWV9f8bk+bTGXHu0TbAbLZUd2opizr56W69/RnyrBnKN0dhOXdzsfmjavah2PtS8JWrG6A371RhUs115BzGtA87NgByLMegcCS80GI/5w3L6/RqEqt3lPt82u/4F0w17X78KO2G3k3b0EtvJ0GfsZ06vFCPAhfFzeNqJJVqLcXsDMWHvh8049K3GeFnt4LZgazVn0NoWwDF/+gry33YL27T66m0xcc+b2VdsX1E4BimjKJRoRK9hh0NtEbP5tZ7mEs7OobZ6DIBXeMah2bc5aEbUBi7EDPQMMHo1zbi1IiZ2F6Dd5QaMWMSHlrXjcJnQDVBJO0RPP/AI/BIH4Wy9LeYvO2l3AfapcGotdwkixvO40TOjo7Vw3lro3u1xCFOc2SfO15xbazXGjrgo4b5fotZyu/1phtZajUGP4c7WGlaGZu4x+0bTaOcW8ldbxuywxbSeHgMipmFGtLyGdmgtp8XsbM7JYWAphtm80HX0OFKLaV7eAz947abW9kTW9pm3tn3RsJm1OWxT3A7m4vvRtk91o30V97Xt5w/7+4b7OjK1PHSCjuYQDH1xf3QbwIVx7AxFbcavxPjBCLu7eHcMvhmN8PRPl2PsA4pxYXrL2b2C3tO+HrfFwte2toZ4j30d8T6bZ5ti/2YgfH076qd73nbu2InHoG1HlBgG+9TO7BPXzFc+CvZTP2nu7VM9ZrM3Z2Nwbu3WFxGtFcaFmMZujzJ2Wg9KRIzNnQvYtOKLtXfCaL3+pdtCfNNkU/6n9Zq1JXHawQMCMvZPWw3taH+aQpTR7E5PFEY7CXOcXYppfs0Q1RhjD5zRh9uh9VyO6UUNNC+cMy3GZnD40t3sfpf/92esIJFv78N9PdLs40nEuGXXpQrc3rzsxEfQnFuHD8SOmFM3dZpXKcY+NcJuJzC1w2Yc+2AwLsVgnN7tbWpFiaBn4Jj2XVabbf+Oy65EbJPsnnez7UXbQwSOaWd27kW1Q9AGb+xzMMc0N9Tmre4X2Il9rMQAeA1loK+1G3tt4ErMzpZha+8Fbz32qd2yoN3lztZui/jegkNMQAL6DgfH+7PWebI3yoT3wJAYHIwxIZ2hLwwDzXjes2V0DsY1GQQ7vWdjaDWHxiCzt86TnffEYxLj77hnvBU3kPNX9HkHWntckuy8ZzNyqSDt4zinG897grANILGD1m48T3beE4Rt4EsyjDJyn7cMlmGa8bxnBkbu85ZRkknpDlcPKeMXdybNYfdti4pv3ds3D1sLhxTrO+coA1rvdnugJeEcLhtnztByV2oNogbKRTIHxtZrxjfKgOjvmAHBgHM43GLQZ7S4N8k4dhxGDcJwBC7HEmCMMw+fPfdz9PiT0zA4tvzarfvTLbihDfRvz7obvlCj30J8ZYex+ZqVZQYNYzDGmTMExJnTNYLRBX5Dqt5DB+NUG60460T3ZkxTDUYcjC/JOImpgVTAM+IAwu6U1MbzkRtlhh47G9x3OMcxwH0MO0PcYATtVrQ77o/o+el3bmwydnBHwtP7zMVT2Zl97hkt0g8twB9HNMyw/e3wzt3dCHMnrX9w8YgdMxwDJjBqB63dmLgH0e2gBCe63i1zzLG3BtGdU884O4a31ptknGgepwm4Hw7Mbhwy7gen9Rq+xMKEOxk37Y67Wfcg8+On43FuRABadhbfG7fHEe7tXC7cab6NIQCHnVkGhxdwxiIu4bxJRjFow4KWgfk5uhjHmtGRc6PM0QTgvkcNkwZjnHVtOzsPcJPftcTgbgdfmuFuZy0obmE7w3eKg6d3R073RFxPxbCM092DvXX0NJK4wQ/sRidsHTMkOSgcwNgPwznYHZywdczAfXfIDEaDM6I1AGN0O2g1z19Lxl0D9DnRNQQ4dsYltD44YBPs9J7tTvY9g923IBbxtjVxbwMB6Lcz63RaB259ZGQGo525ux3A0GoODQ84AE8/5A4O+twnYM8WvLmkfU5vvUnmTYT+Ea8hPRio/diKsyN4tptwzI+D8UvL+N8LpvW0KP8rsLf8EAZf6PG6w/B1wg4nyeDA7FFgkAnDcAc6ZhhcmGY879kyGkOOMPzaMnYAR/e9havthMFxI07t7CTZfg62no299Y+W8dXcsJvS7107G7bw0TV8T4rvXhdkwjCVCqLrOIyTZPs5OLrbmWNnbgTR9U+SMYaLDsLu4Nrfcaf9M2XiLnX83oXRGHY8ZsQeZ+jDE5QwiGToYZSR+6zl7K2TuPOeGRi5z1rO3vp2mZQh46VDyu8mgzvvwHBf74dhA2Iu/hBwb3lAi0ywLsFBBiAMf3VfJHkNcuwH8OvKwC8i/P5HsLO3jonjrBPdPZnhnjg0Y/v0O8ngQR8eS2NNhh9abcux/0TGbmwEGH6Kb0Y9WZziWYuHm3H864k6AGQGdhfvO7SYIVlrhdX5HWSMIWNzabDoAPrRArtxKCHONs/fXcZuQ7+FQdysvl3GnexEl2+sPaPf2EYs4I6H7MZW81EGLbAbexIjurxvGHd0dXbj7ybTPs357QzGYx9go50NagYK2ow4D+s/XsbuOPxcHCs0jNhy+E8oW0DL6Dd0c2gu3hHr8f59Y/s5vIGV3VOPN7TB5Thh75jBPaPL6foGLHbGWcbUv5HM3jPkaH0bu9E9cTAQsKXa4nYj+N1kcNehZ78/B3BH2pl74mAGv2n3jLj1/a+FnWFz4hY7mNdcldPMG2Y41h8GZ4gzMCgOaAzBIOwdM/yDZAaaZ2NwD0Pm3uDy0Pq9ZezedNr5tu7aoa/Q1udhON09jeG+tm4s8EO4dXucux5L3QoYxzaAPoM9j9Pk3r01njf+0TKG9xjutx8G4OKuRJoRxiFn5zeSwV1nxD+k6qvwgHtZf/g7f7U3RvrzOgTABcv29t0gcN8dM3BGwMZemIHWfhgww+8r4+d2eIV5DRlr/KYyduP5amqHI/A1huX3R9zJ4TG5AFgMrOqbWgp8DfgNwwjD0cWBZed3k8HJAY/aD46duYznMeyMtXVAWI78Y2WGWxGYcYgyA9yx/OLFJ2wrfLu8e2KrMvwNsMOWioFx7xuGYQytg6eTxLEDOAT/w2TsADjK/XaOfXE2hIPBsx32c3BMtXPsi7MhHAye7bCfg2OqnWNfnA3hYPBsh/0cDA7xbV5nv1t93bUz3N6OnbkrDnCxFrYjoyQYZMLQ8XS7Ea0djG0obDf+/5TxFthbyDgEwBhnnda3+4EhKgwOcjh769eVsbsO4KaF0aPs4M/rAFr2gScZw11u2Fl/vc/sWMvHOgZ2I4pD4c7eGuLCMGF2hP+GMimeAwcQ9s5uhOdAdDm/qUzclEf8NsUBhL2zG+E5EF3Otoj7nX0JJEFcoQVYqNDHxgJ/9HuzDqQQV2gBFir0sbFAvD98iUL53AIsVOhjY4HfczCcPIU9UynuSz2VUBqnAsDmuRAytpi0DPZUQmmcCgCb50LI2GLSMthTCaVxKgBsnhySLodpcl/bd9I+36Ab0dVJa4PssEcyFirjkEk5mEMS0j6VjFHhrM5w32cwCWmfSsaocFZnuG99MOwJI2BPph6n9MDsuRAytgCMgD2ZepzSA7PnQsjYAjAC9mTqcUoPzJ4LIQ0svzDu34w6YcufbnMc+vwNV/tQS3NPSsyVLYSMpAEMB4BUNk3NLswcvhAykgYwHABS2TQ1uzBz+ELISBrAcABIZdPU7MLM4WmelFVPFYc+wC4LegshL2FPFYc+wC4LegshL2FPFYc+wC4Lejhpy2hbH08/wrEnx2qKFva0aHEZ3OI49OGHRdhm+z8P2/vMfSgi/QjHfTB1CgXf2qrzGcwo9XyrDuIWuFwGS7HLehn38BnMzjOD4UXVF8dtxRuMm237wA4XP67honhnDCjZNFy37HqAKwNs5Kcc8xfNVH9SDlQAqCcDabgyzgq6NzECFQDqyUAaroyzgu5NjEAFgHoykIYr46yQJucETKrBqDKA6lOksptemuczmJ3UhYWeGQwvo9i+4pGC7XBHYlHlpw4pyLllaYTApADStRyebMRg/oxiuKToMrbBjXAekM6NIq0mJfWsGzfQy2YOAaqaFC08Uq9b50QvmzkEqGpStPBIvW6dE71svrUMkCqkqdNwoLOkWTldqghSz7QaXcZIGg50ljQrp0sVQeqZVqPLGEnDwUkWbDz7JrgtdR0YAT+KSB9MABix4DJYqAGv85wzDYcQqmTPeW7SokDqWYhj6gopWqheVOpZiGPqCilaqF5U6lmIY+oKKSWhejWAAxiEq9rScPZU4eCk6hPzOWk1AOGqmjScPVU4OKn6xHxOWg1AuKomDWdPFQ7mqnlpBrxGAn6ugQQAO1zAL6XiEUZaKe+M/Z+O2Qc8UyMqw0d4dOZvm4EypqjwggKnrhuZz2ASVHhBgVPXjcymoJKDzTeBw0GaM82S6gEVfpIzFWbSMkBB6kS4keoBFX6SMxVm0jJAQepEuJHqARV+kpOfQaQPGGwpdBDJD33hyS5pTn6SAdLBYNnm5yGlwaSkGlw+t0Ap+UhBCHBqLkJT0CgoFigIAU7NRWgKGgXFAgUhwKm5iFIZc0gj1U+5nLNuBFvfiXmkkE5xOWfdCLa+E/NIIZ3ics66EWx9vArPW1RvDrvfgfDv8O6X99BqMU5LxALPL2rAE18RwtaFomundwfo5jzswqQBitTzchEnQpfzMGmAIvW8XMSJ0OU8TBqgSD0vFzELcS+TplMoz1QoVSjonbikGqCeHCjPVChVKOiduKQaoJ4cKM9UKFUo6J24wMxrK8AayS0FPKGBtZxLZOxLgf98cWs1hpb9ST/gcjIYpXgZTnZrauYzmIt822BWSWvj5Pe4gMJcrA/tMxjj0cHw+2cFIIXkvKTHupm7AF6v0ycn6EsfmQCW3TbP7FSAk6dSqQtQRrCuwE4F1qUaygjWFdipwLpUQxnBukKaPCVNznEqWSHgi+E6AVMQVskKAV8M1wmYgrBKVgj4YvjcThfqVAqLavqUw9+zM3rOWLD5ZQwIsRHx4T99RNdUJz9qmX/Y1GOIdDAgDQCFuHoykObcWqmZSROANAAU4urJQJpza6VmJk0A0gBQiKsnA2nOrZWaGZUgBS6APbkvZbWk9cjUE2zJG+zJfSmrJa1Hpp5gS95gT+5LWS1pPTL1xPaVny7Dk/tsNR0WXuQMY19wwzaFA2Thrw83/53pBRAcBzg8VQCpkdlc2Jc1QCqlPBmOAxyeKoDUyGwu7MsaIJVSngzHAQ5PFUBqZDaXgi8oSKHFpH2pXhqewjm3ZHP7nNSTjWk1aV+ql4ancM4t2dw+J/VkY1pN2pfqpeEpnHNLhrYvieNimXyEY4/Ecsjb3vT5hIU6aM0rdMudjpdJl2YEzIOpczIpIyonB3CWehyYi1AJUlhYaaRwAGepx4G5CJUghYWVRgoHcJZ6HFBF/NOJv5x1/oywP8otACMo9LGxwF+nvxP0DPyXJPil9oUWgBEU+thY4MdnLLp6boG0XpD2sbHAZyy7UqEF0npB2sfGAr/jWPr/vCj4X3hOSrEctv/KY+5DC2srXCCE1646YUwVrlTWjy/hCtNZS8fCnkoojVMBYPPsx5eoCkFaBXsqoTROBYDNsx9foioEaRXsqYTSOBUANs9+fImqEKRVsKcSSuNUANg8+/ElqkKQVsGeSiiNUwFg8+zHl6gKQVoFeyqhNE4FgM2zH8Ma6yfzF/rIk3/1O6+m6Ps7wrEYg7Q0LNt/RRzW62lk4cLMlfXjaGW4Ly0m7VPJGBXO6gz3fcZyIO1TyRgVzuoM933GciDtU8kYFc7qDPd9xmLwkwVeTXmJZbBQg/4Ig/rSrwFPjoU9YQTsydTjlB6YPftxtDKF3DAC9mTqcUoPzJ79OFqZQm4YAXsy9TilB2bPfhytTCE3jIA9mXqc0gOzZz+OVqaQG0bAnkw9TumB2bMfRytTyA0jYE+mHqf0wOzZj6OVSXPz44b+f34FfWccLX50nD6m8EX854t9crSYubJ+HK3MHDGSBjAcADiZSs0uzBzej6OVKQgqOABwMpWaXZg5vB9HK1MQVHAA4GQqNbswc3g/jlamIKjgAMDJVGp2YebwfhytTEFQwQGAk6nU7MLM4f04WpmCoIIDACdTqdmFmcP7EeaEdI3swIfAgosnwdj9pkvz3/H2AD+OBpNeBDBzZf34knwsGeyp4tAH2GVBrx9fspA7aTHoA+yyoNePL1nInbQY9AF2WdDrx5cs5E5aDPoAuyzo9eNLFnInLQZ9gF0W9PrxJQu5kxaDPsAuC3px9D0t1seTj2nhxNaW35FAS42F49DHqzc/u7hSWRzrpBU+2KrzGcuu9Hyrzmcsu9LzrTq/11hiOayjnixMVbQVdt4ERxZbfmk1HfugcLmy5VcVK2Mher0Bu3Df2/mMZeMzlof4jGXjmbG0ZbPvTW0XO+xAN+Ng6x949htr6890be3PmgP0IRkCOl7E/HiD996AjWiBP/G1Mp3R1AhUAKgnA2m4Ms4K/Zh2JkagAkA9GUjDlXFW6Me0MzECFQDqyUAaroyzQj+mnYkRqABQTwbScGWcFfox7UyMQAWAejKQhivjrNCPaWdiBCoA1JOBNFwZZ4V+0sBSOb1EHC2QvvYGEIB1l9+74EfH/Nbx8JNCeNlHX7YH0/xjQF78t7EQPBdMOl2MmlGg+hSp7KYXR+Yzlo3PWEp8xnLGrzwWWtZ4MeZtKJbFaVVMIRdkyZd7EEbeWMOTjRiL5RxW6r74R19lKkE6Mwp1QZjUs27soJOtHAFUMSlSl6iXLXOik60cAVQxKVKXqJctc6KTrRwBVDEpUpeoly1zopOtHAFUMSlSl6iXLXOik60cAVQxKVKXqJctc6KTrRyRr6YBP94Yniy4AfEBK2C97s8nQgHgJ34cjl3ztAUPzy0LPNEM5OiJ1JONIFVIU6fhQGZJk3K2VBCknmkxsgoiDQcyS5qUs6WCIPVMi5FVEGk4kFnSpJwtFQSpZ1qMrIJIw4HMkiblbKkgSD3TYmQVRBoOZJY0KWdLBUHqmRYjqyDScHCSJdYzLHK+urX/TblxXPkOCy5a6Vh4iQVYPwFvujlnGj485TAmT3z/kk6lmsO0BTiOqSukaCGy1pXSFpglRuoKKVqIrHWltAVmiZG6QooWImtdKW2BWWKkrpCihchaV0pbYJYYqSukaCGy1pXSFpglRuoKKVooljPelKoHDDD2pB60xQFITEssiCzD2urh8SFXaBAugBf4bYVG7kDNDOAABuFqmtNw9lThIK86twrSYgDCVTFpOHuqcJBXnVsFaTEA4aqYNJw9VTjIq86tgrQYgHBVTBrOnioc5FXnVkFaDEC4KiYNZ08VDvKqc6sgLQYgXBWThrOnCgdz1bGeqS0xmF6/QIIAiziwcPcY9t7WxkscAFVMz4xJthdKRizU8XH4UtCPI+mU8CQwKlwlYxdQNzKfsRxQ4SoZu4C6kfmM5YAKV8nYBdSNzO81llgVseNU/1yDl20G4dtOdTTuS7M/D2EX/FhuqtCCXDAaWLY9/FAE1vL+FSGS9Wxq9DyxDIeDNGeaJdUDKjzPmeoyaRWgoJTrBqkeUOF5zlSXSasABaVcN0j1gArPc6a6TFoFKCjlukGqB1R4njPVZdIqQEEp1w1SPaDC85ypLpNWAQpKuW6Q6gEVfpIzVrX+tCJa/MqDL5XGsGHdW70YMqY5p+chQTqWfTF2sEKXxpKRSqTzxJ6V3ERBCHBqLkJSkCgIFigIAU7NRUgKEgXBAgUhwKm5CElBoiBYoCAEODUXISlIFAQLFIQAp+YiJAWJgmCBghDg1FzE9Dvt+2oKYlXk3ejxebLBDxHM2H5GGEw5gz/Cg1dvJcRGfEWYF+pognRG59E3eLoUl3PWjWDrQzu4rFvgcs66EWx9aAeXdQtczlk3gq0P7eCyboHLOetGsPWhHVzWLXA5Z90Itj60g8u6BS7nrBvB1hdrXfpEl//tBzasgFdhrJjTs4RA7YzzCkOBn6PAkxfjLhR9nXSgnIZdmDRAkXpeLiIXupyGSQMUqeflInKhy2mYNECRel4uIhe6nIZJAxSp5+UicqHLaZg0QJF6Xi4iF7qchkkDFKnn5SJmIVrkOm1lph/SzT8GnIhcaixYTQHvcGPX3BWwsvcsYewre7Qml05YU+CqKmSUJ88oSBUKeicuYU2p5wbKE31MqlDQO3EJa0o9N1Ce6GNShYLeiUtYU+q5gfJEH5MqFPROXMKaUs8NlCf6mFShoHfiEtaUem6gPNHHpAoFvROXsPJGt0PrJ1qKbKXtzyfSsaQK1Go79fljEtogay54FU52a2rmM5ZLfMaywGcsl/i2scRizNvQAtPqHfBD3159W1v7MsrPNVQWJvXkLxPpg+sK6TTzPN3jAgqXNY2r8BnLZyzOZyw1fsmxYEErw9veaX/tDvPOmJfY/iAkXKan2QHGgv+Bb3o9L2DZroCvlXW6UtYCqQtQRrCuEM0660oNZQTrCtGss67UUEawrhDNOutKDWUE6wrRrLOu1FBGsK4QzTrrSg1lBOsK0Uz3pgwWwDxN5tI33WhgiQ0bvz9ReJAMvW35HY3TWLbmORw4p2mkRlAI+GL4ZyyHFvhiMV8M/4zl0AJfLOaL4b/xWC7/v9X5yxi+Wd63y9ayPrUYJw87xsfHHs5PkDkcRcjfnwy4r48lSANAIa6eDKQ5t1Y/jlYmjQdKvhBXTwbSnFurH0crk8YDJV+IqycDac6t1Y+jlUnjgZIvxNWTgTTn1urH0cqk8UDJF+LqyUCac2v142hl0nig5Atx9WQgzbm1+nG0Mmm8rYrDUslgM5s+Vu4PfcmlE0Z+IoFWJ7LwYoyW/B1EIB1L6gngAtiT+1IKQmlJ89fKaDFpYOoJttwN9uS+lIJQWtJnLHsrZcvdYE/uSykIpSV9xrK3UrbcDfbkvpSCUFrSvWPhxRjAk/v4l2dgoeb3ofn5RH9oEXSFaH11n6w8GY4DHJ4qgNTIbC79OFqZVEl5MmkxHJ4qgNTIbC79OFqZVEl5MmkxHJ4qgNTIbC79OFqZVEl5MmkxHJ4qgNTIbC79OFqZVEl5MmkxHJ4qgNTIbC79OFqZVEl5MmkxHJ4qgNTIbC79CHMC73fZk/e0vji2999Ghj3tvg73dTdgBeyFwf48ZGBbxBv5PllRmLVeDJH2pXppeArn3JJNzXPmwAYb02LSvlQvDU/hnFuyqXnOHNhgY1pM2pfqpeEpnHNLNjXPmQMbbEyLSftSvTQ8hXNuyabmOXNgg41pMWlfqpeGp3DOLdnUPGcObLAxLSbtS/XS8BTOuSWLpi95+4qZfRye6Pp625dD3remjxsQPm+XW25+USMl3SefjKXMyZSMqJo4gLPU48BnLDusqyRSOOAzlo3PWHZYV0mkcMDNY8EqV4aX395ioi9lCmBjtFKU0Nb3R7SfI60e3NTHwCfjpjhYn8P0fsQ99xw/6J/X/5v07ulj2JO5J0553oPpvUNjYHXuVR9Tn1+mHqc878H03qExsDr3qo+pzy9Tj1Oe92B679AYWJ171cfU55epxynPezC9d2gMrM696mPq88vU45TnPZjeOzQGVude9TH1+WXqccrzHkyPNeKZyRfhF0vUmH7+DxvDxv8oUMVx339or89ZpmfqYqP8x19watTH8NCcifGuouawfq+puHrO1RHV9VYVmEPOd2gMqDlk6nOhcq6OqK63qsAccr5DY0DNIVOfC5VzdUR1vVUF5pDzHRoDag6Z+lyonKsjquutKjCHnO/QGFBzyNTnQuVcHVFdb1WBOeR8h8aAmkOmPhcq5+qI6nqrCswhp9LAzvM1vP1UdfNulFF7U477628IN6adMVGftf8gW/A36fH+Wl4XxL/mxZwJDfZVqLj6zNTjVitTrKqrsStUnPW9Q6NIPW61MsWquhq7QsVZ3zs0itTjVitTrKqrsStUnPW9Q6NIPW61MsWquhq7QsVZ3zs0itTjVitTrKqrsStUnPW9Q6NIPW61MsWquhq7QsVZ3zs0Bng3yqi9qdrFKnhHPYHcwb//REiD4/74D5wav+N1UTnZk1E5FU/orY6PeZHzHRoDKid7Miqn4gm91fExL3K+Q2NA5WRPRuVUPKG3Oj7mRc53aAyonOzJqJyKJ/RWx8e8yPkOjQGVkz0ZlVPxhN7q+JgXOd+hMaBysiejciqe0FsdH/Mi5zs0Bng3yg+C+S0Mhp8ZsyfvYpn62xT3PE/+p14Xpq6gUAqMqmy1apVF8UL9HRqn1BUUSoFRla1WrbIoXqi/Q+OUuoJCKTCqstWqVRbFC/V3aJxSV1AoBUZVtlq1yqJ4of4OjVPqCgqlwKjKVqtWWRQv1N+hcUpdQaEUGFXZatUqi+KFutD4g/a0igvvJRDqLQxmerUC1mB6ZozcGfW3N1TVany3zVl5DuvUr0sdlXNVj+MYleUt43uHxg2onKt6HMeoLG8Z3zs0bkDlXNXjOEZlecv43qFxAyrnqh7HMSrLW8b3Do0bUDlX9TiOUVneMr53aNyAyrmqx3GMyvKW8VFbPdGto3ac03sX9CR46oM1WL0uSm+6Lti/BvX3Lh6aM67tAepz+M/sewLTe4fGwPfO4RN9T2B679AY+N45fKLvCUzvHRoD3zuHT/Q9gem9Q2Pge+fwib4nML13aAx87xw+0fcEpld+/rnK6jsLqu/C78kQbx2rOH7yzGOoPwVfhfXewT3XRcFxjMqi4n5/Ptfl1+RzXX5NPtfl1+RzXX5NPtelCP/+ZBiD6VksA59AehL83u/qvpUcJziOK2MFZnoujPgM9dybUZ5qzmybvHiP1j2ZVQXmicqYuvqq54sxrEcO1Ee/qsA8URlTV1/1fDGG9ciB+uhXFZgnKmPq6queL8awHjlQH/2qAvNEZUxdfdXzxRjWIwfqo19VYJ6ojKmrr3q+GMN65EB99KsKzBOVMXX1Vc8XY6Be9Zx2ei+BnxKLp9JTEq5NxLEC7yrV77tQe2/1u47ZUb1NwTmV5/RdgZizwyyV7yd1Xyjqd5di9R5lVuNWqY/2ML5yNc/PqOJzXc54fkYVn+tyxvMzqvhclzOen1HF57qc8fyMKv7fXRexW+NdJb8LoJ6GMuo3R9ThXeVUy+P/z8gEfALlyddFzZmepXvuSqZ+z6yy+jdEUc/5hOcMRypfpcGszkyd9fGe88Rsr9fJkcpXaTCrM1NnfbznPDHb63VypPJVGszqzNRZH+85T8z2ep0cqXyVBrM6M3XWx3vOE7O9XidHKl+lwazOTJ318Z7zxGyv18mRyldp1PemjNx7i3/xp96mUGOY9GAN1DvI8ok1wc+a60/B5ym757roa39OPae8K4j6GOpV19WZ9Vrqlara6iNk6jnrM7M+F+fU1Zn1WuqVqtrqI2TqOeszsz4X59TVmfVa6pWq2uojZOo56zOzPhfn1NWZ9Vrqlara6iNk6jnrM7M+F+fU1Zn1WuqVqtrqI2TqOeszsz4X59TVmUu10LPY6T0B2rsx9T2f/J0W6vdPENN1Ee9rMPUxqPc8eELr/46Pt9C8a+bfocE57XsLcc/U54lR92G9j1F6iifGUOcLI3pgntSY6n3Mi1Gc8sQY6nxhRA/MkxpTvY95MYpTnhhDnS+M6IF5UmOq9zEvRnHKE2Oo84URPTBPakz1PubFKE55Ygx1vjCiB+ZJjanex7wYxSlPjKHOF0ZEO075pBQ+gXpKzLvY6a1cqlS+D03wKNRum1FvU6y+Waz20Ez9qTSjnyczq/cMoxQUrL56N9fVVc5VdebKXF/xXaM+Mwyrr85MXV3lXFVnrsz1Fd816jPDsPrqzNTVVc5VdebKXF/xXaM+Mwyrr85MXV3lXFVnrsz1Fd816jPDsPrqzNTVVc5VdebKXF/xXaM+Mwyrr85MXV3lXFVnXsy1eJ7MqKfEzLSPJNRemHfiDKvL596oImOaC8QHcg8tnnsrz9WZsOtSvvb1u0TdCYpV9dXKVBbmCU+FZbllTMwXqjlFqa9WprIwT3gqLMstY2K+UM0pSn21MpWFecJTYVluGRPzhWpOUeqrlakszBOeCstyy5iYL1RzilJfrUxlYZ7wVFiWW8bEfKGaU5T6amUqC/OEp8KyiOfJq/+7htptK5Q6P5m9sGsWWfiZuJpD+dwb2QJVGTNtk2ENrvw7vvq157g6Sp2p11mvpT4+ZlX9Sp318Srqc8HcM6Yr4x2pj49ZVb9SZ328ivpcMPeM6cp4R+rjY1bVr9RZH6+iPhfMPWO6Mt6R+viYVfUrddbHq6jPBXPPmK6Md6Q+PmZV/Uqd9fEq6nPB3DOmK+MdqY+PWVW/UqfaR6o3GJhpD434QL2zwJWqLOq3X6i3PhQXrgtyB7wz5j30XdelTn0U9ftJ5byr7nPuGRGjqlYzsc49o7hnhPdwz4gYVbWaiXXuGcU9I7yHe0bEqKrVTKxzzyjuGeE93DMiRlWtZmKde0Zxzwjv4Z4RMapqNRPr3DOKe0Z4D/eMiFFVq5kw+P8ZYeCSIXfNsAbqqa18Tkuo59fqHRBGvdd8z4iUJ6u/2FEvXm3mxbUfUHfXKs/X+YQnc4i74nvKPbWt8nydT3gyh7grvqfcU9sqz9f5hCdziLvie8o9ta3yfJ1PeDKHuCu+p9xT2yrP1/mEJ3OIu+J7yj21rfJ8nU94Moc4evrKu8Npb0q7St7zyTcfCLUX5j2m2qkyq8+M63PIM6HeR+GcamesdtQz9SuqqlFZFHWFVeo5n5+JKyN6vhpFXWGVes7nZ+LKiJ6vRlFXWKWe8/mZuDKi56tR1BVWqed8fiaujOj5ahR1hVXqOZ+fiSsjer4aRV1hlXrO52fixYjU/9FB8F5R7YxX309Wz2nr12V6ZkyoZ7hq5z9tcClu9V2VFyMSV1TBWVfnULGaU92jTH0M94zvUpbyKJgn6mZWc3Kcoj6Ge8Z3KUt5FMwTdTOrOTlOUR/DPeO7lKU8CuaJupnVnBynqI/hnvFdylIeBfNE3cxqTo5T1Mdwz/guZSmPgnmibmY1J8cp6mO4Z3yXsvAosKMLxKZZvjEh35EoU9/T8pPu+nWpj+Ge8akRHSjfJd+LquzXrVrxuS6/Jp/r8mvyuS6/Jp/r8mvyuS6/Ji+ui/gXcfw09B5oq6hBRKCeNas9rXpfY7UWRT2n2m3LlzAeoX43q/vpV8rC3PO3ta53F5/rUuFzXWp8rsvO57q8k891qfC5LjW+4bqof8d3D6g4mOrG7jLgPea0j1TPjBGfwW+E8P5T/ss9YrouyBZwTnldoBRMerAG9l3BLddXwWPiulUfU8/CrHoy3zAGkecevmFMA6uezDeMQeS5h28Y08CqJ/MNYxB57uEbxjSw6sl8wxhEnnv4hjENrHoy3zAGkecevmFMA6uezDeMgfNgn/Y1eAdYr6b+Nq/6vXC8i+X3jNlz+Y1k9VvpxHwqT31dDr1LKMUX1QzUPZl7FL5X3VjUV9wz3u+dme9VNxb1FfeM93tn5nvVjUV9xT3j/d6Z+V51Y1Ffcc94v3dmvlfdWNRX3DPe752Z71U3VC92kK9Rz1Q5J++F678ZQz1dVvvd+s5Y7WLrbySrd5CV+uG5t5hDrk2h4qZrTdQVmHv0nqiMqdd56Cv7Kur6zOro79F7ojKmXuehr+yrqOszq6O/R++Jyph6nYe+sq+irs+sjv4evScqY+p1HvrKvoq6PrM6+nv0nqiMqdd56Cv7Kur6zOro79F7ojKmXuehr+yrqOvzXnF6l1iB/WTAvwmO9aY9LXwC+RyakO9kwBqoIXAtamd82LO/+brUczKchVE5VVyde0ZUnyXjzYr1nMyh7gGVU8XVuWdE9Vky3qxYz8kc6h5QOVVcnXtGVJ8l482K9ZzMoe4BlVPF1blnRPVZMt6sWM/JHOoeUDlVXJ17RlSfJePNivWczKHuAZVTxdW5Z0T1WTLerFjPqd5ZYKaciAh4R8279InFZ9RKneE49WTdHG+ZwyeuC8cplB6j1OtjYOqeikMWkVWNgqmPaTWnoj4zSr0+BqbuqThkEVnVKJj6mFZzKuozo9TrY2DqnopDFpFVjYKpj2k1p6I+M0q9Pgam7qk4ZBFZ1SiY+phWcyrqM6PU62Ng6p6KQxaRVY2CqY9pNaeiPjNKvT4Gpu6pOGQRWfk9XIXaOU7jpZzqOa16J4NRz2kZ9mQuPKMm5PshDHIHL3bit1xt5p57jfsU9bj6+OrqdVSdh8p07xIvFAeUZ31m6nH18dXV66g6D5Xp3iVeKA4oz/rM1OPq46ur11F1HirTvUu8UBxQnvWZqcfVx1dXr6PqPFSme5d4oTigPOszU4+rj6+uXkfVeahM9y7xQnFAedZnph5XH19dvY6q81AZ9dbfw1WsvqOrnrfW326Y3uVAfMAK09Plxd/CPAGlDLUT19flCS7dJaeszpNSULU8ocd8rssZn+uy87kunc91OeNzXXY+16XzuS5n/NLXpfzMeJVpd1h+Q5jhXXMdpcC1qDrr1EekZuJf//o/fyPqzGelmBgAAAAASUVORK5CYII=";
                //    return Ok(respuesta);
                //}
                //else
                //{
                //    CuerpoRespuesta respuesta = new CuerpoRespuesta();
                //    respuesta.hayError = true;
                //    respuesta.mensajeError = "Petición no válida";
                //    respuesta.qrBase64 = "";
                //    return Ok(respuesta);
                //}
            }
            catch (Exception ex)
            {
                CuerpoRespuesta respuesta = new CuerpoRespuesta();
                respuesta.hayError = true;
                respuesta.mensajeError = ex.Message;
                respuesta.qrBase64 = "";
                return Ok(respuesta);
            }
        }
        //public async Task<ActionResult<CuerpoRespuesta>> Post([FromBody] CuerpoPeticion objPeticion)
        //{
        //    try
        //    {
        //        objPeticion.ValidarPropiedades();
        //        //return Ok(QR.GenerarQR(objPeticion));

        //        if (objPeticion.referencia == "SL202100007822")
        //        {
        //            CuerpoRespuesta respuesta = new CuerpoRespuesta();
        //            respuesta.hayError = false;
        //            respuesta.mensajeError = "";
        //            respuesta.qrBase64 = "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";
        //            return Ok(respuesta);
        //        }
        //        else
        //        {
        //            CuerpoRespuesta respuesta = new CuerpoRespuesta();
        //            respuesta.hayError = true;
        //            respuesta.mensajeError = "Petición no válida";
        //            respuesta.qrBase64 = "";
        //            return Ok(respuesta);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CuerpoRespuesta respuesta = new CuerpoRespuesta();
        //        respuesta.hayError = true;
        //        respuesta.mensajeError = ex.Message;
        //        respuesta.qrBase64 = "";
        //        return Ok(respuesta);
        //    }
        //}

        // PUT api/<CodiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CodiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}