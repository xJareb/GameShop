using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Endpoint.Igrice.Izdvoji
{
    [Tags("Igrice")]
    public class IgriceIzdvojiEndpoint : MyBaseEndpoint<IgriceIzdvojiRequest, IgriceIzdvojiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IgriceIzdvojiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPut("IzdvojiIgricu")]
        public override async Task<IgriceIzdvojiResponse> Obradi([FromQuery]IgriceIzdvojiRequest request, CancellationToken cancellationToken = default)
        {
            var igrica = _applicationDbContext.Igrice.Where(i=>i.Id == request.IgricaID).FirstOrDefault();
            if(igrica == null)
                throw new Exception("Nije pronađena igrica za id: " +  request.IgricaID);

            igrica.Izdvojeno = request.Izdvojeno;

            _applicationDbContext.Update(igrica);
            await _applicationDbContext.SaveChangesAsync();

            return new IgriceIzdvojiResponse()
            {
                IgricaID = request.IgricaID
            };
        }
    }
}
