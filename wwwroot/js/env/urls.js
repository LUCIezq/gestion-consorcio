const GEOREF_URL_BASE = "https://apis.datos.gob.ar/georef/api";
const URL_MAPBOX = 'https://api.mapbox.com/search/searchbox/v1';

export const URLS = {
    provincias: `${GEOREF_URL_BASE}/provincias?campos=id,nombre`,
    municipios: (id, max = 1) => `${GEOREF_URL_BASE}/municipios?provincia=${id}&campos=id,nombre&max=${max}`
}

export const URLS_MAPBOX = {
    obtenerDireccion: (query, accessToken, session_token) => `${URL_MAPBOX}/suggest?q=${encodeURIComponent(query)}&access_token=${accessToken}&session_token=${session_token}&country=AR&language=es&limit=1`,
    obtenerCoordenadas: (mapBoxId, accessToken, session_token) => `${URL_MAPBOX}/retrieve/${mapBoxId}?access_token=${accessToken}&session_token=${session_token}`
}