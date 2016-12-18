namespace YimaEncCtrl
{
    public enum CURRENT_SUB_OPERATION
    {
        NO_OPERATION = 0,
        ADD_FORBIDDEN_ZONE = 1,
        ADD_PIPELINE = 0x2,
        RANGING = 0x4,
        AREA_ZOOM_IN = 0x10,
        ADD_WAYPOINT = 0x20,
        ADD_ROUTE = 0x40,
        EDITING_ROUTE = 0x80,
        EDITING_WAY_POINT = 0x100,
        HAND_ROAM = 0x200,
        SHOWING_HINT_TEXT = 0x400,
        EDITING_BASIC_OBJECT = 0x2000,
        EDITING_EDGE_MID_PO = 0x4000,
        DRAG_EDITING_OBJECT = 0x8000,
        EDITING_GEO_OBJECT = 0x10000,
        DRAW_EBL = 0x40000,
        ADD_USER_LAYER_OBJ = 0x80000,
        AREA_SELECT = 0x100000,
        SHOWING_TRACK = 0x200000,
        PLAYBACK = 0x400000,
        ADD_LINE_OBJECT_MOUSEMOVING = 0x1000000, //添加物标时的动态节点添加状态
    }

    public enum SPECIAL_LINE_TYPE
    {
        NO_SPECIAL_TYPE = 0,
        RECTANGLE_LN_TYPE = 10,
        CIRCLE_LN_TYPE = 20,
        ELLIPSE_LN_TYPE = 30,
        ARC_LN_TYPE = 40,
        PIE_LN_TYPE = 41,
        BOW_LN_TYPE = 42,
        SECTOR_LN_TYPE = 50,
        CURVE_LN_TYPE = 60,
        CURVE_LN_TYPE_WITH_HEAD_ARROW = 61,
        CURVE_LN_TYPE_WITH_HAED_TAIL_ARROW = 62,
        CLOSED_CURVE_LN_TYPE = 70,
        SINGLE_ARROW_LN_TYPE = 80,
        DOUBLE_ARROW_LN_TYPE = 90,
        THREE_ARROW_LN_TYPE = 100
    }

    public enum LAYER_GEO_TYPE
    {
        LAYER_GEO_TYPE_NULL = 0,
        ALL_POINT = 1,
        ALL_LINE = 2,
        ALL_FACE = 3,
        MULTIPLE_GEO_TYPE = 5
    }

    public enum M_GEO_TYPE //物标的几何属性
    {
        TYPE_NULL = -1,
        TYPE_POINT = 0,
        TYPE_LINE = 2,
        TYPE_FACE = 3,
        TYPE_COMBINED_OBJECT = 10
    }

    //图层显示
    public enum DISPLAY_CATEGORY_NUM
    { 
        DISPLAY_BASE = 0,
        DISPLAY_STANDARD = 1,
        DISPLAY_ALL = 2,
    }

    //显示模式
    public enum DISPLAY_MODE
    {
        READER,
        AIS,
        MERGER,
        RADARLINE,
        OPTLINE
    }

    //移动海图的方向
    public enum MovingDirection
    {
        Up, 
        Down, 
        Right, 
        Left
    }
}
