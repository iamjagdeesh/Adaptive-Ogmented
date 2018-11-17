package com.application.pojo;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;

@Entity
@Table(name = "USER_TBL")
public class User {
    @Id
    @Column(name = "USER_ID")
    private String userId;

    @Column(name = "PASSWORD")
    private String password;

    @Column(name = "LEVEL_OF_EXPERTISE")
    private Integer levelOfExpertise;

    public User() {

    }

    public User(String userId, String password, Integer levelOfExpertise) {
        this.userId = userId;
        this.password = password;
        this.levelOfExpertise = levelOfExpertise;
    }

    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public Integer getLevelOfExpertise() {
        return levelOfExpertise;
    }

    public void setLevelOfExpertise(Integer levelOfExpertise) {
        this.levelOfExpertise = levelOfExpertise;
    }
}
