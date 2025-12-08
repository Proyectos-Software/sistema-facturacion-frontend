using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class MenuDataService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public MenuDataService(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    private List<MainMenuItems> MenuData = new List<MainMenuItems>()
    {
        // ───────────────────────────
        // ENCABEZADO PRINCIPAL
        // ───────────────────────────
        new MainMenuItems(menuTitle: "Main"),

        // Dashboard (opcional para landing interna)
        new MainMenuItems(
            type: "sub",
            title: "Dashboards",
            svg: "<svg xmlns='http://www.w3.org/2000/svg' class='side-menu__icon' viewBox='0 0 256 256'><rect width='256' height='256' fill='none'/><path d='M32 216H224' stroke='currentColor' stroke-width='16' stroke-linecap='round'/><rect x='40' y='56' width='80' height='112' rx='8' fill='none' stroke='currentColor' stroke-width='16'/><rect x='136' y='96' width='80' height='72' rx='8' fill='none' stroke='currentColor' stroke-width='16'/></svg>",
            selected: false, active: false, dirChange: false,
            children: new MainMenuItems[]
            {
                new MainMenuItems(
                    path: "",
                    type: "link",
                    title: "Resumen",
                    selected: false, active: false, dirChange: false,
                    roles: new[] { "Admin","Empleado" }
                ),
            }
        ),

// ───────────────────────────
// SECCIÓN: GESTIONES
// ───────────────────────────
new MainMenuItems(menuTitle: "Gestiones"),

new MainMenuItems(
    type: "sub",
    title: "Gestiones",
    svg: "<svg xmlns='http://www.w3.org/2000/svg' class='side-menu__icon' viewBox='0 0 256 256'><rect width='256' height='256' fill='none'/><path d='M40 64h176v128H40z' fill='none' stroke='currentColor' stroke-width='16'/> <path d='M40 104h176' stroke='currentColor' stroke-width='16'/></svg>",
    selected: false, active: false, dirChange: false,
    children: new MainMenuItems[]
    {
        // Gestión de Clientes
        new MainMenuItems(
            path: "/dashboards/clientes",
            type: "link",
            title: "Gestión de Clientes",
            svg: "<svg xmlns='http://www.w3.org/2000/svg' class='side-menu-doublemenu__icon' viewBox='0 0 256 256'><circle cx='96' cy='96' r='32' fill='none' stroke='currentColor' stroke-width='16'/><path d='M24 208a64 64 0 0 1 128 0' fill='none' stroke='currentColor' stroke-width='16'/></svg>",
            roles: new[] { "Admin","Empleado" }
        ),

        // Gestión de Productos
        new MainMenuItems(
            path: "/dashboards/productos",
            type: "link",
            title: "Gestión de Productos",
            svg: "<svg xmlns='http://www.w3.org/2000/svg' class='side-menu-doublemenu__icon' viewBox='0 0 256 256'><rect x='40' y='64' width='176' height='128' rx='8' fill='none' stroke='currentColor' stroke-width='16'/><path d='M40 104h176' stroke='currentColor' stroke-width='16'/></svg>",
            roles: new[] { "Admin","Empleado" }
        ),

        // Gestión de Empleados
        new MainMenuItems(
            path: "/dashboards/empleados",
            type: "link",
            title: "Gestión de Empleados",
            svg: "<svg xmlns='http://www.w3.org/2000/svg' class='side-menu-doublemenu__icon' viewBox='0 0 256 256'><circle cx='128' cy='88' r='28' fill='none' stroke='currentColor' stroke-width='16'/><path d='M64 208a64 64 0 0 1 128 0' fill='none' stroke='currentColor' stroke-width='16'/></svg>",
            roles: new[] { "Admin" }
        ),

        // Gestión de Lotes
        new MainMenuItems(
            path: "/dashboards/lotes",
            type: "link",
            title: "Gestión de Lotes",
            svg: "<svg xmlns='http://www.w3.org/2000/svg' class='side-menu-doublemenu__icon' viewBox='0 0 256 256'><rect x='36' y='56' width='184' height='144' rx='8' fill='none' stroke='currentColor' stroke-width='16'/><path d='M36 100h184M84 56v144M172 56v144' fill='none' stroke='currentColor' stroke-width='16' stroke-linecap='round'/></svg>",
            roles: new[] { "Admin","Empleado" }
        ),

        // Gestión de Facturas
        new MainMenuItems(
            path: "/dashboards/facturas",
            type: "link",
            title: "Gestión de Facturas",
            svg: "<svg xmlns='http://www.w3.org/2000/svg' class='side-menu-doublemenu__icon' viewBox='0 0 256 256'><rect x='36' y='56' width='184' height='144' rx='8' fill='none' stroke='currentColor' stroke-width='16'/><path d='M36 100h184M84 56v144M172 56v144' fill='none' stroke='currentColor' stroke-width='16' stroke-linecap='round'/></svg>",
            roles: new[] { "Admin","Empleado" }
        ),
    }
), // ← ESTE PARÉNTESIS ERA EL QUE FALTABA

// ───────────────────────────
// SECCIÓN: REPORTES
// ───────────────────────────
new MainMenuItems(menuTitle: "Reportes"),

new MainMenuItems(
    type: "sub",
    title: "Reportes",
    svg: "<svg xmlns='http://www.w3.org/2000/svg' class='side-menu__icon' viewBox='0 0 256 256'><rect width='256' height='256' fill='none'/><path d='M40 200h176' stroke='currentColor' stroke-width='16'/><rect x='48' y='48' width='64' height='120' rx='8' fill='none' stroke='currentColor' stroke-width='16'/><rect x='144' y='96' width='64' height='72' rx='8' fill='none' stroke='currentColor' stroke-width='16'/></svg>",
    children: new MainMenuItems[]
    {
        new MainMenuItems(
            path: "/dashboards/reporte-general",
            type: "link",
            title: "Reportes Generales",
            roles: new[] { "Admin", "Empleado" }
        ),
        new MainMenuItems(
            path: "/dashboards/auditorias",
            type: "link",
            title: "Auditorías - General",
            roles: new[] { "Admin" }
        ),
        
    }
),



        // ───────────────────────────
// SECCIÓN: CONFIGURACIÓN
// ───────────────────────────
new MainMenuItems(menuTitle: "Configuración"),

new MainMenuItems(
    type: "sub",
    title: "Configuración del Sistema",
    svg: "<svg xmlns='http://www.w3.org/2000/svg' class='side-menu__icon' viewBox='0 0 256 256'><rect width='256' height='256' fill='none'/><path d='M168.6 118a40.1 40.1 0 0 0 0 20 8 8 0 0 1-5.7 9.8l-14.2 3.8a8 8 0 0 1-9.6-4.1 40.1 40.1 0 0 0-13.8-13.8 8 8 0 0 1-4.1-9.6l3.8-14.2a8 8 0 0 1 9.8-5.7 40.1 40.1 0 0 0 20 0 8 8 0 0 1 9.6 4.1l14.2 3.8a8 8 0 0 1 5.8 9.9z' stroke='currentColor' stroke-width='16'/> </svg>",
    selected: false,
    active: false,
    dirChange: false,
    roles: new[] { "Admin" }, // ✨ Solo visible para Administradores
    children: new MainMenuItems[]
    {
        new MainMenuItems(
            path: "/dashboards/configuracion/empresa",
            type: "link",
            title: "Empresa",
            selected: false,
            active: false,
            dirChange: false,
            roles: new[] { "Admin" }
        ),
        new MainMenuItems(
            path: "/dashboards/configuracion/facturacion-electronica",
            type: "link",
            title: "Facturación Electrónica",
            selected: false,
            active: false,
            dirChange: false,
            roles: new[] { "Admin" }
        ),
        new MainMenuItems(
            path: "/dashboards/configuracion/certificado-digital",
            type: "link",
            title: "Certificado Digital",
            selected: false,
            active: false,
            dirChange: false,
            roles: new[] { "Admin" }
        )
    }
),

            
        // (Opcional) Otras páginas base si quieres mantener
        // new MainMenuItems(menuTitle: "Pages"),
        // ...

    };

    public List<MainMenuItems> GetMenuData() => MenuData;

    public async Task<List<MainMenuItems>> GetFilteredMenuAsync()
    {
        // Obtiene el usuario actual
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        // Carga la lista maestra
        var masterMenu = GetMenuData();

        // Filtra la lista
        return FilterMenuForUser(masterMenu, user);
    }

    // 4. Lógica de filtrado recursiva
    private List<MainMenuItems> FilterMenuForUser(IEnumerable<MainMenuItems> menuItems, ClaimsPrincipal user)
    {
        var filteredList = new List<MainMenuItems>();

        foreach (var item in menuItems)
        {
            bool hasPermission = (item.Roles == null || item.Roles.Length == 0) ||
                                 item.Roles.Any(role => user.IsInRole(role));

            if (!hasPermission)
            {
                continue;
            }

            if (item.Type == "sub" && item.Children != null)
            {
                var visibleChildren = FilterMenuForUser(item.Children, user);

                if (visibleChildren.Count > 0)
                {
                    var newItem = new MainMenuItems
                    {
                        MenuTitle = item.MenuTitle,
                        Type = item.Type,
                        Title = item.Title,
                        Svg = item.Svg,
                        Path = item.Path,
                        Selected = item.Selected,
                        Active = item.Active,
                        DirChange = item.DirChange,
                        Roles = item.Roles,
                        Children = visibleChildren.ToArray()
                    };
                    filteredList.Add(newItem);
                }
            }
            else
            {
                filteredList.Add(item);
            }
        }
        return filteredList;
    }

}
