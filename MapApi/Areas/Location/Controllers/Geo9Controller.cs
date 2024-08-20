﻿using MapApi.Locations;
using NewLife;
using NewLife.Cube;
using NewLife.Cube.ViewModels;
using NewLife.Web;
using XCode.Membership;

namespace MapApi.Areas.Location.Controllers;

[Menu(80, true, Icon = "product-hunt")]
[LocationArea]
public class Geo9Controller : EntityController<Geo9>
{
    static Geo9Controller()
    {
        ListFields.RemoveCreateField();
        ListFields.RemoveField("ProvinceId", "CityId", "DistrictId");

        {
            var df = ListFields.GetField("Code") as ListField;
            df.Url = "/Location/Geo9?code={Code}";
        }
    }

    protected override IEnumerable<Geo9> Search(Pager p)
    {
        var code = p["code"].ToInt(-1);
        var rids = p["areaId"].SplitAsInt("/");
        var provinceId = rids.Length > 0 ? rids[0] : -1;
        var cityId = rids.Length > 1 ? rids[1] : -1;
        var districtId = rids.Length > 2 ? rids[2] : -1;

        var start = p["dtStart"].ToDateTime();
        var end = p["dtEnd"].ToDateTime();

        return Geo9.Search(code, provinceId, cityId, districtId, start, end, p["Q"], p);
    }
}