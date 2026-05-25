const GEOREF_URL_BASE = "https://apis.datos.gob.ar/georef/api";

export const URLS = {
    provincias: `${GEOREF_URL_BASE}/provincias?campos=id,nombre`,
    municipios: (id, max = 1) => `${GEOREF_URL_BASE}/municipios?provincia=${id}&campos=id,nombre&max=${max}`
}