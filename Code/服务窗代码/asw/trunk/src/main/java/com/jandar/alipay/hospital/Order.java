package com.jandar.alipay.hospital;

public class Order {
    private String orderseq;
    private String ordertime;
    private String orderno;

    public String getOrderseq() {
        return orderseq;
    }

    public void setOrderseq(String orderseq) {
        this.orderseq = orderseq;
    }

    public String getOrdertime() {
        return ordertime;
    }

    public void setOrdertime(String ordertime) {
        this.ordertime = ordertime;
    }

    public String getOrderno() {
        return orderno;
    }

    public void setOrderno(String orderno) {
        this.orderno = orderno;
    }

    public Order(String orderseq, String ordertime, String orderno) {
        this.orderseq = orderseq;
        this.ordertime = ordertime;
        this.orderno = orderno;
    }
    
}
