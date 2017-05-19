package com.jandar.alipay.core.struct;

/*
 * 
 * 就诊卡信息
 */
public class MedicineUseInfo {
    private String ypmc;   // |药物名称 | string | 是    | |
    private String ypgg;   // | 药物规格 | string | 是    |     |
    private String ypsl;   //  | 药物数量  | string | 是    |             |
    private String ycyl;   //| 一次用量  | string | 是    |          |
    private String ysjy;  //   医生建议
    private String sync;  //  | 使用频次
    private String gytj;  // | 给药途径
    /**
     * 以下为扩展协议
     * @return
     */
    private String tzybz; //|特种药标志 | string | 是    |处方中是否包含特种药,是为Yes，否为No |
    public String getYsjy() {
        return ysjy;
    }

    public void setYsjy(String ysjy) {
        this.ysjy = ysjy;
    }

    public MedicineUseInfo() {
        super();
    }


    public String getYpmc() {
        return ypmc;
    }


    public void setYpmc(String ypmc) {
        this.ypmc = ypmc;
    }


    public String getYpgg() {
        return ypgg;
    }


    public void setYpgg(String ypgg) {
        this.ypgg = ypgg;
    }


    public String getYpsl() {
        return ypsl;
    }


    public void setYpsl(String ypsl) {
        this.ypsl = ypsl;
    }


    public String getYcyl() {
        return ycyl;
    }


    public void setYcyl(String ycyl) {
        this.ycyl = ycyl;
    }


    public String getSync() {
        return sync;
    }


    public void setSync(String sync) {
        this.sync = sync;
    }


    public String getGytj() {
        return gytj;
    }


    public void setGytj(String gytj) {
        this.gytj = gytj;
    }

    /**
     * 以下为扩展协议
     * @return
     */
    public String getTzybz() {
        return tzybz;
    }

    public void setTzybz(String tzybz) {
        this.tzybz = tzybz;
    }
}
