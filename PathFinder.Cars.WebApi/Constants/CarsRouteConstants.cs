using PathFinder.Infrastructure.Constants;

namespace PathFinder.Cars.WebApi.Constants.RouteConstants
{
    public static class CarsRouteConstants
    {
        // Segments
        public const string Brands = "/brands";

        public const string Colors = "/colors";

        public const string Models = "/models";

        public const string Cars = "/cars";

        // Parameters
        public const string BrandId = "/{brandId:int}";

        // Controller prefixes
        public const string CarBrandsControllerPrefix = CommonRouteConstants.RouteBase + Brands;

        public const string CarColorsControllerPrefix = CommonRouteConstants.RouteBase + Colors;

        public const string CarModelsControllerPrefix = CommonRouteConstants.RouteBase + Models;

        public const string CarsControllerPrefix = CommonRouteConstants.RouteBase + Cars;

        // Method routes
        public const string GetBrand = CommonRouteConstants.Id;

        public const string GetBrands = CommonRouteConstants.Empty;

        public const string GetColor = CommonRouteConstants.Id;

        public const string GetColors = CommonRouteConstants.Empty;

        public const string GetModel = CommonRouteConstants.Id;

        public const string GetCar = CommonRouteConstants.Id;

        public const string GetModelsByBrandId = CommonRouteConstants.Tilde +
                                                 CommonRouteConstants.RouteBase + Brands + BrandId + Models;

        public const string GetCarsByBrand = CommonRouteConstants.Tilde +
                                             CommonRouteConstants.RouteBase + Brands + BrandId + Cars;
    }
}
