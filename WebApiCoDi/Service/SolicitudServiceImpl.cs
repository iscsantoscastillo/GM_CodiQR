using WebApiCoDi.Repository;

namespace WebApiCoDi.Service
{
    public class SolicitudServiceImpl: ISolicitudService
    {
        private ISolicitudRepo _iSolicitudRepo;
        public SolicitudServiceImpl(ISolicitudRepo solicitudRepo) {
            _iSolicitudRepo = solicitudRepo;
        }
        public bool existeSolicitud(string claveSolicitud) {
            return _iSolicitudRepo.existeSolicitud(claveSolicitud);
        }
    }
}
